using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CountriesController : Controller
    {
        private readonly AppDbContext _context;
        public CountriesController(AppDbContext context)
        {
            _context = context;
        }

        [Route("{country}")]
        public IActionResult Index(string country)
        {
            var model = new IndexPage();

            model.Country = _context.Country.FirstOrDefault(m => m.Name == country);
            if (model.Country == null)
                return RedirectToAction("Index", "Home");

            model.Embassies = (from e in _context.Embassy
                               join c in _context.Country
                               on e.Country.Id equals c.Id
                               where e.OriginalCountry.Name == country
                               select new EmbassyVM
                               {
                                   Id = e.Id,
                                   Country = c.Name,
                                   City = e.City.Name,
                                   Iata = c.ISOalpha3.ToLower()
                               }).Distinct().ToList();

            model.Visas = (from v in _context.Visa
                           where v.Country.Id == model.Country.Id
                           select new VisaSearchResult
                           {
                               Id = v.Id,
                               Description = v.Description,
                               VisaName = v.Name,
                               IsExdendable = v.IsExtendable,
                               Duration = v.Duration
                           }).ToList();
            foreach(var v in model.Visas)
            {
                v.Reviews = _context.Review.Where(r => r.Visa.Id == v.Id).ToList();
            }

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();

            var topDuration = model.Visas.OrderByDescending(m => m.Duration).FirstOrDefault();
            var days90 = model.Visas.OrderByDescending(m => m.Duration >= 90).ToList();
            //VisaSearchResult evisa = Model.Visas.Where(m => m.ev)

            header.Text = $"{model.Visas.Count} types of tourist visas for {model.Country.Name} are presented. ";

            if (topDuration != null)
                header.Text += $"The longest period of stay is {topDuration.Duration} days. ";
            if (days90.Count == 1)
                header.Text += $"Good options for digital nomads on {model.Country.Name} is {string.Join(", ", days90.Select(r => r.VisaName))}. ";
            if(days90.Count >= 2)
                header.Text += $"Good options for digital nomads on {model.Country.Name} are {string.Join(", ", days90.Select(r => r.VisaName))}. ";


//            если есть рейтинг
//The most popular visa among Glomad's users is the {Visa name} up to {duration} days.

//Евиза может прийти только если локация есть.
//Если есть  евиза в спике
//{ citezen}
//            citezens can get E-visa up to { duration}
//            days for travelling to { country Name}.

            //The most popular is the Tourist visa up to 90 days.
            //In addition, it is possible to apply for E - visa for 90 days.

            //Our users best rated the Russian embassies in @String.Join(", ", top.Select(r => r.Name)) Baku, Azerbaijan_, Minsk, Belarus, Brussels, Belgium.

            //if (model.Country.UpdateDate != null)
            //    header.Text += $"Last update: {model.Country.UpdateDate.Value.ToString("")}";
            //< p id = "updatedate" data - utcdate = "@Model.Country.UpdateDate.Value.ToString("s", System.Globalization.CultureInfo.InvariantCulture);" ></ p >

            model.Header = header;

            return View(model);
        }

        [Route("{country}/Embassies")]
        public IActionResult Embassies(string country)
        {
            var model = new EmbassiesPage();

            model.Country = _context.Country.FirstOrDefault(m => m.Name == country);

            model.Embassies = (from e in _context.Embassy
                               join c in _context.Country
                               on e.Country.Id equals c.Id
                               where e.OriginalCountry.Name == country
                               select new EmbassyVM
                               {
                                   Id = e.Id,
                                   Country = c.Name,
                                   City = e.City.Name,
                                   Iata = c.ISOalpha3.ToLower()
                               }).Distinct().ToList();

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();
            header.Text = $"List of Embassies and Consulates of {header.CountryName}. You can get a visa to {header.CountryName} in {model.Embassies.Count} embassies around the world. Find a list of {header.CountryName} embassies around the world below.";

            model.Header = header;

            return View("Embassies", model);
        }

        [Route("{country}/Covid")]
        public IActionResult Covid(string country)
        {
            var model = new CovidPage();

            model.Country = _context.Country.FirstOrDefault(m => m.Name == country);
            model.Covid = _context.Country.FirstOrDefault(c => c.Id == model.Country.Id).CovidRestrictions;

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();
            header.Text = $"Up-to-date info COVID-19 travel restrictions. Quarantine conditions, entry requrements, list of approved vaccines and etc., help you make decisions about future trips in {DateTime.Now.Year}.";
            model.Header = header;

            return View("Covid", model);
        }

        [Route("{country}/NoEntry")]
        public IActionResult NoEntry(string country)
        {
            return RedirectToActionPermanent(actionName: "NoVisaEntry", controllerName: country);
        }

        [Route("{country}/NoVisaEntry")]
        public IActionResult NoVisaEntry(string country)
        {
            ViewBag.LastUpdates = _context.Country.Where(c => c.Name == country).Select(r => r.UpdateDate.Value);

            var model = new FreeEntry();

            model.countries = (from ne in _context.NoVisaEntry
                     join co in _context.Country on ne.CountryDestination.Id equals co.Id
                     where co.Name == country
                     select new CountryFreeEntry
                     {
                         Details = ne.Description,
                         Id = co.Id,
                         Iata = ne.CountryPassport.ISOalpha3 != null ? ne.CountryPassport.ISOalpha3 : "No data",
                         Name = ne.CountryPassport.Name != null ? ne.CountryPassport.Name : "No data",
                         EVisaAvailable = ne.IsEVisaAvailable,
                         IsVisaRequired = ne.IsVisaRequired,
                         Duration = ne.Duration,
                         EVisaUrl = ne.EVisaUrl
                     }).OrderByDescending(d => d.Duration).ToList();

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();

            int evisa = model.countries.Where(CountryFreeEntry => CountryFreeEntry.EVisaAvailable == true).Count();
            var top = model.countries.OrderByDescending(CountryFreeEntry => CountryFreeEntry.Duration).Take(3).ToList();

            header.Text = $"There are {model.countries.Count} countries whose citizens are not required to obtain a visa to visit {header.CountryName}, ";
            if (evisa > 0)
                header.Text += $"including {evisa} countries that need to get an E - Visa before the entry. ";
            header.Text += $"Citizens of {String.Join(", ", top.Select(r => r.Name))} are allowed to stay in {header.CountryName} without a visa for up to {top.First().Duration} days.";

            model.header = header;

            return View("NoVisaEntry", model);
        }

        [Route("{country}/FreeEntry")]
        public IActionResult FreeEntry(string country)
        {
            FreeEntry model = new FreeEntry();

            string countryName = country.FirstCharToUpper();
            string citizen = _context.Country.Where(c => c.Name == country).FirstOrDefault().Citizen;

            model.countries = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where co.Name == country && ne.IsVisaRequired == false
                             select new CountryFreeEntry
                             {
                                 Details = ne.Description,
                                 Id = ne.CountryDestination.Id,
                                 Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                                 Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                                 EVisaAvailable = ne.IsEVisaAvailable,
                                 IsVisaRequired = ne.IsVisaRequired,
                                 Duration = ne.Duration,
                                 EVisaUrl = ne.EVisaUrl
                             }).OrderByDescending(d => d.Duration).ToList();

            HeaderViewModel header = new HeaderViewModel();
            header.Text = $"There are {model.countries.Count} visa-free countries available for {citizen ?? countryName}. ";

            int evisa = model.countries.Where(CountryFreeEntry => CountryFreeEntry.EVisaAvailable == true).Count();
            if (evisa > 0)
                header.Text += $"These also include {evisa} countries that only require easily obtainable E - visas. ";

            var top = model.countries.OrderByDescending(CountryFreeEntry => CountryFreeEntry.Duration).Take(3);
            header.Text += $"The countries, which offer the most extended visa - free stay period (i.e., {top.First().Duration} days), " +
                           $"are {string.Join(", ", top.Select(r => r.Name))}.";

            header.CountryName = countryName;

            model.header = header;

            return View("FreeEntry", model);
        }

        [Route("Country/CreateReview")]
        [HttpPost("CreateReview")]
        public IActionResult CreateReview([FromBody] ReviewCreate reviewCreate)
        {
            try
            {
                Visa v = _context.Visa.FirstOrDefault(c => c.Id == reviewCreate.VisaId);
                Embassy e = _context.Embassy.FirstOrDefault(e => e.Id == reviewCreate.EmbassyId);
                Review r = new()
                {
                    Visa = v,
                    Embassy = e,
                    Cons = reviewCreate.Cons,
                    Pros = reviewCreate.Pros,
                    Loyalty = reviewCreate.Loyalty,
                    Simplicity = reviewCreate.Simplicity,
                    Waiting = reviewCreate.Waiting,
                    IsObtained = reviewCreate.IsObtained,
                    Date = DateTime.Now,
                    Likes = 0
                };

                _context.Review.Add(r);
                _context.SaveChanges();
                Helpers.EmailSender.SendReview(reviewCreate);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("{country}/Visa/{id}")]
        public IActionResult Visa(string country, int id)
        {
            VisaPage model = new VisaPage();

            model.Reviews = _context.Review.Where(r => r.Visa.Id == id).ToList();
            model.Visa = _context.Visa.Where(v => v.Id == id).FirstOrDefault();
            model.Visa.Country = _context.Country.Where(c => c.Name.ToLower() == country.ToLower()).FirstOrDefault();

            return View("Visa", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
