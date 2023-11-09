using API.Helpers;
using API.Models;
using Glomad.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
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
        //protected override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);
        //    // your code here
        //}

        //private void IncreaseViewCouter(int countryId)
        //{
        //    if (Request.IsLocal()) return;
        //    //var Country = _context.Country.FirstOrDefault(m => m.Id == countryId);

        //    _context.SaveChanges();
        //}

        private void IncreaseViewCouter(object o)
        {
            if (Request.IsLocal()) return;

            if (o is Visa)
            {
                var Visa = (Visa)o;
                Visa.ViewCounter = Visa.ViewCounter + 1;
            }

            if(o is Country)
            {
                var Country = (Country)o;
                Country.ViewCounter = Country.ViewCounter + 1;
            }

            
            _context.SaveChanges();
        }



        [Route("{country}")]
        [Route("{country}/{citizen}")]
        public async Task<IActionResult> Index(string country, string citizen)
        {
            string countryName = country.FirstCharToUpper();

            if (string.IsNullOrEmpty(countryName))
                return NotFound();

            var Country = _context.Country.FirstOrDefault(m => m.Name == countryName);
            if (Country == null)
                return NotFound();
            //return RedirectToAction("Index", "Home");

            //IncreaseViewCouter(model.Country.Id);
            var model = new IndexPage();
            model.Country = Country;

            IncreaseViewCouter(model.Country);

            model.Visas = (from v in _context.Visa
                           where v.Country.Id == model.Country.Id
                           select new VisaSearchResult
                           {
                               Id = v.Id,
                               Description = v.Description,
                               VisaName = v.Name,
                               IsExdendable = v.IsExtendable,
                               Duration = v.Duration,
                               CountryName = country,
                               Type = VisaTypes.Types[v.Type],
                               Income = v.Income,
                               Cost = $"{v.CostOfProgramm} {v.CostCurrency}",
                               IsAnnounced = v.IsAnnounced
                           }).ToList();
            foreach (var v in model.Visas)
            {
                v.Reviews = _context.Review.Where(r => r.Visa.Id == v.Id).ToList();
            }

            DateTime? lastVisaUpdate = null;
            if (model.Visas.Any())
                 lastVisaUpdate = model.Visas.Max(u => u.UpdateDate);


            string myCountry;
            if (string.IsNullOrEmpty(citizen))
                myCountry = await new GeoIp(HttpContext).GetMyCountryAsync(_context);
            else
                myCountry = citizen;


            try
            {
                model.HomeCountry = _context.Country.Where(c => c.Name.ToLower().Replace(" ", "") == myCountry.ToLower().Replace(" ", "")).FirstOrDefault();
            }
            catch
            {

            }
            


            try
            {
                model.NoVisaEntry = _context.NoVisaEntry
                .Where(i => i.CountryDestination.Id == model.Country.Id && i.CountryPassport.Id == model.HomeCountry.Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                model.NoVisaEntry = null;
            }
            

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();
            header.HomeCountryName = citizen;
            header.Text = model.Country.Summary;

            if (model.Country.UpdateDate.HasValue || lastVisaUpdate != null) {
                header.LastModifiedHeader = (DateTime)new[] { model.Country.UpdateDate, lastVisaUpdate }.Max();
                Response.Headers.Add("Last-Modified", value: header.LastModifiedHeader.ToUniversalTime().ToString("R"));
            }




            // NEXT COUNTRIES
            if(!string.IsNullOrEmpty(Country.NextCountries))
            {
                int[] nextCountries = Country.NextCountries.Split(',').Select(int.Parse).ToArray();
                model.NextCountries = (from item in _context.Country
                                       where nextCountries.Contains(item.Id)
                                       select item).ToList();
            }
            

            //if (!string.IsNullOrEmpty(model.Country.NextCountries))
            //{
            //    string[] countryIds = model.Country.NextCountries.Split(',');
            //    int[] intCountryIds = new int[countryIds.Length];

            //    for (int i = 0; i < countryIds.Length; i++)
            //    {
            //        intCountryIds[i] = int.Parse(countryIds[i]);
            //    }

            //    var query = from item in _context.Country
            //                where intCountryIds.Contains(item.Id)
            //                select item;

            //    var nextCountries = query.ToList();
            //    model.NextCountries = nextCountries;
            //}

            // -- NEXT COUNTRIES


            if (model.Visas.Count > 0)
            {
                var topDuration = model.Visas.OrderByDescending(m => m.Duration).FirstOrDefault();
                var days90 = model.Visas.OrderByDescending(m => m.Duration >= 90).ToList();
                //VisaSearchResult evisa = Model.Visas.Where(m => m.ev)

                header.Text += $"{model.Visas.Count} types of tourist visas for {model.Country.Name} are presented. ";

                if (topDuration != null)
                    header.Text += $"The longest period of stay is {topDuration.Duration} days. ";
                if (days90.Count == 1)
                    header.Text += $"Good options for digital nomads on {model.Country.Name} is {string.Join(", ", days90.Select(r => r.VisaName))}. ";
                if (days90.Count >= 2)
                    header.Text += $"Good options for digital nomads on {model.Country.Name} are {string.Join(", ", days90.Select(r => r.VisaName))}. ";
            }

            model.Header = header;

            model.EmbassiesOfCountryInMyCountry = (from e in _context.Embassy
                                           where e.OriginalCountry.Id == model.Country.Id && e.Country.Id == model.HomeCountry.Id
                                           select new EmbassyVM
                                           {
                                               Id = e.Id,
                                               Country = model.Country.Name,
                                               City = e.City.Name,
                                               Iata = model.Country.ISOalpha3.ToLower(),
                                               Latitude = e.Latitude,
                                               Longitude = e.Longitude,
                                               Embassy = e
                                           }).Distinct().ToList();


            if (string.IsNullOrEmpty(citizen))
            {
                ViewBag.Title = $"{model.Country.Name} visa types: Visa requirements, check lists of documents, {model.Country.Citizen} list of visa types in {DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)} of {DateTime.Now.Year}";

                ViewBag.Description = $"The list of visa types for {country}. {country} tourist, business, work, student, invest visas. " +
                    $"Validity, duration of stay, number of entries, and other parameters in " +
                    $"{DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)} of {DateTime.Now.Year} " +
                    $"for {model.Country.Citizen} visit visas.";
            }
            else
            {
                if(model.NoVisaEntry != null && model.NoVisaEntry.IsVisaRequired)
                {
                    try
                    {
                        model.TouristVisa = _context.Visa.Where(v => v.Country.Id == model.Country.Id && v.Type == 2 && !v.IsFreeVisa).FirstOrDefault();

                        if (model.TouristVisa != null)
                        {
                            model.TouristVisaDocs = _context.VisaDoc.Where(vd => vd.Visa.Id == model.TouristVisa.Id).ToList();
                        }
                    }
                    catch(Exception e)
                    {                        
                    }                    
                }

                model.EmbassiesOfMyCountryInCountry = (from e in _context.Embassy
                                                       where e.OriginalCountry.Id == model.HomeCountry.Id && e.Country.Id == model.Country.Id
                                                       select new EmbassyVM
                                                       {
                                                           Id = e.Id,
                                                           Country = model.HomeCountry.Name,
                                                           City = e.City.Name,
                                                           Iata = model.HomeCountry.ISOalpha3.ToLower(),
                                                           Latitude = e.Latitude,
                                                           Longitude = e.Longitude,
                                                           Embassy = e
                                                       }).Distinct().ToList();

                ViewBag.Title = $"Visas of {model.Country.Name} for citizens of {citizen}: Visa requirements, check lists of documents, {model.Country.Citizen} list of visa types in {DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)} of {DateTime.Now.Year}";

                ViewBag.Description = model.NoVisaEntry?.Description.StripHTML().GetUntilOrEmpty("Important!").Trim();
            }
            ViewBag.MyCountry = citizen;


            return View(model);
        }

        public static T Max<T>(T first, T second)
        {
            if (Comparer<T>.Default.Compare(first, second) > 0)
                return first;
            return second;
        }

        [Route("{country}/Embassies")]
        public IActionResult Embassies(string country)
        {
            string countryName = country.FirstCharToUpper();

            var Country = _context.Country.Where(c => c.Name == countryName).FirstOrDefault();
            if (Country == null)
            {
                return NotFound();
            }

            var model = new EmbassiesPage();
            model.Country = Country;

            IncreaseViewCouter(model.Country.Id);

            model.Embassies = (from e in _context.Embassy
                               join c in _context.Country
                               on e.Country.Id equals c.Id
                               where e.OriginalCountry.Name == country
                               select new EmbassyVM
                               {
                                   Id = e.Id,
                                   Country = c.Name,
                                   City = e.City.Name,
                                   Iata = c.ISOalpha3.ToLower(),
                                   Latitude = e.Latitude,
                                   Longitude = e.Longitude,
                                   Embassy = e
                               }).Distinct().ToList();

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();
            header.Text = $"List of Embassies and Consulates of {header.CountryName}. ";
            if(model.Embassies.Count > 0)
            {
                header.Text += $"You can get a visa to {header.CountryName} in {model.Embassies.Count} embassies around the world. Find a list of {header.CountryName} embassies around the world below. ";
            }
            else
            {
                header.Text += $"We are working to prepare an actual list of embassies.. Stay tuned! ";
            }

            model.Header = header;

            return View("Embassies", model);
        }

        [Route("{country}/Covid")]
        public async Task<IActionResult> Covid([FromServices] AmadeusAPI amadeusAPI, string country)
        {
            string countryName = country.FirstCharToUpper();

            var Country = _context.Country.Where(c => c.Name == countryName).FirstOrDefault();
            if (Country == null)
            {
                return NotFound();
            }


            var model = new CovidPage();

            IncreaseViewCouter(Country.Id);

            var amadeus = _context.AmadeusApi.SingleOrDefault();
            
            if (amadeus.LastCall < DateTime.Now.AddMonths(-1)) {
                amadeus.CallsLimit = 0;
                amadeus.LastCall = DateTime.Now;              
                _context.SaveChanges();
                amadeus = _context.AmadeusApi.SingleOrDefault();
            }

            if (amadeus.CallsLimit < 180 && (Country.UpdateDate == null || Country.UpdateDate < DateTime.Now.AddDays(-15)))
            {
                if (!string.IsNullOrEmpty(Country.ISOalpha2))
                {
                    await amadeusAPI.ConnectOAuth();
                    try
                    {
                        var results = await amadeusAPI.GetTravelRestrictions(Country.ISOalpha2);
                        Country.AmadeusTravelRestrictions = JsonSerializer.Serialize(results);
                        Country.UpdateDate = DateTime.Now;
                        amadeus.CallsLimit = amadeus.CallsLimit + 1;
                        _context.SaveChanges();
                        model.AmadeusTravelRestrictions = results;
                    }
                    catch (NotImplementedException e)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            else
            {
                model.AmadeusTravelRestrictions = JsonSerializer.Deserialize<AmadeusTravelRestrictions>(Country.AmadeusTravelRestrictions);
            }
            if(model.AmadeusTravelRestrictions?.data?.areaAccessRestriction?.entry?.bannedArea != null)
            {
                var iatas = model.AmadeusTravelRestrictions.data.areaAccessRestriction.entry.bannedArea.Select(a => a.iataCode).ToList();
                model.BannedCountries = _context.Country.Where(t => iatas.Contains(t.ISOalpha2)).Select(t => t.Name).ToList();
            }

            model.Country = Country;

            string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync(_context);
            model.HomeCountry = myCountry;//_context.Country.Where(c => c.ISOalpha2 == myCountry).FirstOrDefault().Name;

            model.Covid = _context.Country.FirstOrDefault(c => c.Id == model.Country.Id).CovidRestrictions;

            model.ApprovedVaccines = _context.ApprovedVaccines.Where(c => c.CountryId == model.Country.Id).Select(i => i.VaccineId).ToList();

            model.Restrictions = _context.CovidRestrictions.Where(r => r.Country.Id == model.Country.Id)
                .ToDictionary(i => i.Restriction, i => i.Level);

            //Dictionary<int, int> RestrictionLevel = _context.CovidRestrictions.Where(r => r.Country.Id == model.Country.Id)
            //    .ToDictionary(i => i.Restriction, i => i.Level);

            //model.RestrictionPair = 

            var header = new HeaderViewModel();
            header.CountryName = country.FirstCharToUpper();
            header.Text = $"Up-to-date info COVID-19 travel restrictions. Quarantine conditions, entry requrements, list of approved vaccines and etc., help you make decisions about future trips in {DateTime.Now.Year}.";

            if(Country.UpdateDate != null)
                header.LastUpdate = Country.UpdateDate.Humanize();

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
            string countryName = country.FirstCharToUpper();

            var Country = _context.Country.Where(c => c.Name == countryName).FirstOrDefault();
            if (Country == null)
            {
                return NotFound();
            }

            IncreaseViewCouter(Country.Id);

            ViewBag.LastUpdates = Country.UpdateDate.Value;

            //IncreaseViewCouter(curCountry.Id);

            var model = new FreeEntry();

            model.countries = (from ne in _context.NoVisaEntry
                     join co in _context.Country on ne.CountryDestination.Id equals co.Id
                     where co.Name == country && (!ne.IsVisaRequired || ne.IsEVisaAvailable)
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

            header.Text = $"There are {model.countries.Count} countries whose citizens are not required to obtain a visa to visit {header.CountryName}";
            if (evisa > 0)
                header.Text += $" , including {evisa} countries that need to get an E - Visa before the entry. ";
            if(top.Count > 0)
                header.Text += $"Citizens of {String.Join(", ", top.Select(r => r.Name))} are allowed to stay in {header.CountryName} without a visa for up to {top.First().Duration} days.";
            else
                header.Text += $"Everybody have to apply to visa of {header.CountryName} in prior.";

            model.header = header;

            return View("NoVisaEntry", model);
        }

        [Route("{country}/FreeEntry")]
        public IActionResult FreeEntry(string country)
        {            
            string countryName = country.FirstCharToUpper();

            var Country = _context.Country.Where(c => c.Name == country).FirstOrDefault();
            if(Country == null)
            {
                return NotFound();
            }

            IncreaseViewCouter(Country.Id);

            FreeEntry model = new FreeEntry();

            string citizen = Country.Citizen;

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
            
            if(model.countries.Any())
            {
                int evisa = model.countries.Where(CountryFreeEntry => CountryFreeEntry.EVisaAvailable == true).Count();
                if (evisa > 0)
                    header.Text += $"These also include {evisa} countries that only require easily obtainable E - visas. ";

                var top = model.countries.OrderByDescending(CountryFreeEntry => CountryFreeEntry.Duration).Take(3);
                header.Text += $"The countries, which offer the most extended visa - free stay period (i.e., {top.First().Duration} days), " +
                               $"are {string.Join(", ", top.Select(r => r.Name))}.";
            }
            header.CountryName = countryName;

            model.header = header;

            return View("FreeEntry", model);
        }

        [Route("{country}/passport")]
        public IActionResult Passport(string country)
        {
            string countryName = country.FirstCharToUpper();

            PassportViewModel model = new PassportViewModel();
            model.Country = _context.Country.FirstOrDefault(m => m.Name == country);

            List<CountryFreeEntry> allFreeCountries = (from ne in _context.NoVisaEntry
                                                       join co in _context.Country on ne.CountryPassport.Id equals co.Id
                                                       where co.Name == country && (!ne.IsVisaRequired || ne.IsEVisaAvailable)
                                                       select new CountryFreeEntry
                                                       {
                                                           Details = ne.Description,
                                                           Id = co.Id,
                                                           Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                                                           Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                                                           EVisaAvailable = ne.IsEVisaAvailable,
                                                           IsVisaRequired = ne.IsVisaRequired,
                                                           Duration = ne.Duration,
                                                           EVisaUrl = ne.EVisaUrl
                                                       }).OrderBy(d => d.Name).ToList();

            model.CountryFreeEntry = allFreeCountries.Where(c => c.IsVisaRequired == false).ToList();
            model.CountryEvisa = allFreeCountries.Where(c => c.EVisaAvailable).ToList();

            var header = new HeaderViewModel();
            header.Text = $"Visa free countries, e-Visa countries and the most popular countries to travel for citezens of {countryName}";
            header.CountryName = country.FirstCharToUpper();
            model.Header = header;


            int[] popCountries = _context.PopularCountries.Where(c => c.Id == model.Country.Id).Select(c => c.Country.Id).ToArray();
            //model.PopularCountries?.Split(',')?.Select(int.Parse)?.ToList();

            //var countryIds = model.Country.PopularCountries.Split(',');
            //var intCountryIds = new int[countryIds.Length];

            //for (int i = 0; i < countryIds.Length; i++)
            //{
            //    intCountryIds[i] = int.Parse(countryIds[i]);
            //}

            model.PopularCountries = (
                from pc in _context.PopularCountries
                join c in _context.Country on pc.Country.Id equals c.Id
                where pc.Citizenship.Id == model.Country.Id
                //where popCountries.Contains(c.Id)
                select new PopularCountriesViewModel
                {
                    Id = pc.Id,
                    Name = c.Name,
                    Iata = c.ISOalpha3,
                    Reason = pc.Reason
                }).Distinct().ToList();

            return View(model);
        }

        [Route("{country}/FAQ")]
        public IActionResult Faq(string country)
        {
            FaqPage model = new FaqPage();

            string countryName = country.FirstCharToUpper();

            var Country = _context.Country.Where(c => c.Name == country).FirstOrDefault();
            IncreaseViewCouter(Country.Id);

            string citizen = Country.Citizen;

            model.CountryCapitalCode = Country.CapitalCode;

            model.questions = (from cc in _context.CountryQuestion
                               where cc.Country.Id == Country.Id
                               select new CountryQuestion
                               {
                                   Text = cc.Text,
                                   Answer = cc.Answer,
                                   Country = Country
                               }).ToList();

            HeaderViewModel header = new HeaderViewModel();
            header.Text = $"Questions & Answers about {countryName} Visa";
            header.CountryName = countryName;
            model.header = header;

            return View("Faq", model);
        }

        [Route("{country}/Auto"),
        Route("{country}/Season"),
        Route("{country}/Safety"),
        Route("{country}/Guide"),
        Route("{country}/Info")]
        public IActionResult OtherInfo(string country)
        {
            CountryBasicVM model = new CountryBasicVM();

            string countryName = country.FirstCharToUpper();

            model.Country = _context.Country.Where(c => c.Name == country).FirstOrDefault();
            IncreaseViewCouter(model.Country.Id);

            HeaderViewModel header = new HeaderViewModel();
            header.Text = "";
            header.CountryName = countryName;
            model.Header = header;

            return View("OtherInfo", model);
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
                    Text = reviewCreate.Text,
                    Loyalty = reviewCreate.Loyalty,
                    Simplicity = reviewCreate.Simplicity,
                    Waiting = reviewCreate.Waiting,
                    IsObtained = reviewCreate.IsObtained,
                    Date = DateTime.Now,
                    Likes = 0
                };

                _context.Review.Add(r);
                _context.SaveChanges();
                EmailSender.SendReview(reviewCreate);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("Country/CreateVisaReport")]
        [HttpPost("CreateVisaReport")]
        public IActionResult CreateVisaReport([FromBody] ReportVisa report)
        {
            try
            {
                EmailSender.SendVisaReport(report);
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

            var Country = _context.Country.Where(c => c.Name == country).FirstOrDefault();

            //IncreaseViewCouter(Country.Id);

            model.Reviews = _context.Review.Where(r => r.Visa.Id == id).ToList();
            model.Visa = _context.Visa.Where(v => v.Id == id).FirstOrDefault();
            model.Visa.Country = Country; //_context.Country.Where(c => c.Name.ToLower() == country.ToLower()).FirstOrDefault();
            model.VisaDocs = _context.VisaDoc.Where(v => v.Visa.Id == id).ToList();

            IncreaseViewCouter(model.Visa);

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
                           where v.Country.Name.ToLower() == country.ToLower()
                           select new VisaSearchResult
                           {
                               Id = v.Id,
                               Description = v.Description,
                               VisaName = v.Name,
                               IsExdendable = v.IsExtendable,
                               Duration = v.Duration
                           }).ToList();
            
            int[] nextCountries = Country.NextCountries.Split(',').Select(int.Parse).ToArray();

            model.SameVisasOtherCountries = (from v in _context.Visa
                                             join c in _context.Country
                                             on v.Country.Id equals c.Id
                                             where (v.Type == model.Visa.Type || v.IsFreeVisa)
                                             && nextCountries.Contains(c.Id) && v.IsActual && v.Duration >= 180
                                             select new SameVisasOtherCountries
                                             {
                                                 Country = c.Name,
                                                 VisaId = v.Id,
                                                 VisaName = v.Name
                                             }
                             ).ToList();

            var allVisasSameType = (from v in _context.Visa
                                    join c in _context.Country on v.Country.Id equals c.Id
                                    join d in _context.VisaDoc on v.Id equals d.Visa.Id
                                    where c.Id != Country.Id && v.IsActual && v.Type == model.Visa.Type
                                    select new SameVisasOtherCountries
                                    {
                                        Country = c.Name,
                                        VisaId = v.Id,
                                        VisaName = v.Name,
                                        DocType = d.DocumentType,
                                        Duration = v.Duration,
                                        Type = v.Type,
                                        Income = v.Income
                                    }).Distinct().ToList();
            
            if (model.Visa.Type == 3 && model.Visa.IsActual == false) { 
                
                model.AnalogVisa = (from v in _context.Visa
                                    join c in _context.Country on v.Country.Id equals c.Id
                                    
                                    where c.Id == Country.Id && v.IsActual && v.Duration >= 90 && (v.Type == (int)VisaType.Startup || v.Type == (int)VisaType.Student || v.Type == (int)VisaType.Tourist  || v.Type == (int)VisaType.Retreat || v.Type == (int)VisaType.Business)
                                    select new SameVisasOtherCountries
                                    {                                                                                                                       
                                        Country = c.Name,
                                        VisaId = v.Id,
                                        VisaName = v.Name,                                    
                                        Duration = v.Duration,
                                        Type = v.Type,
                                        Income = v.Income
                                    }).Distinct().ToList();
            }

            //if look nomad visa, we offer other long visas
            if (model.Visa.Type == 3) {
                    model.YearLongVisas = (from v in _context.Visa
                                           join c in _context.Country on v.Country.Id equals c.Id
                                          // join d in _context.VisaDoc on v.Id equals d.Visa.Id
                                           where c.Id != Country.Id && v.IsActual && v.Duration >= 360 && (v.Type == (int)VisaType.Startup || v.Type == (int)VisaType.Student || v.Type == (int)VisaType.Tourist)
                                           select new SameVisasOtherCountries
                                           {
                                               Country = c.Name,
                                               VisaId = v.Id,
                                               VisaName = v.Name,
                                               //DocType = d.DocumentType,
                                               Duration = v.Duration,
                                               Type = v.Type,
                                               Income = v.Income
                                           }).Distinct().ToList(); 
                }

            foreach (var v in allVisasSameType)
            {

                // our visa is Digital nomad
                /*if (model.Visa.Type == 3 && v.Duration > 360 && v.Type != 3 && !model.YearLongVisas.Any(i => i.VisaId == v.VisaId))
                    model.YearLongVisas.Add(v);*/

                if (v.Income <= model.Visa.Income && v.Type == model.Visa.Type && !model.CheapVisas.Any(i => i.VisaId == v.VisaId) && model.Visa.Type!=(int)VisaType.Tourist)
                    model.CheapVisas.Add(v);

                if (!model.VisaDocs.Any(v => v.DocumentType == ((int)DocumentType.Criminal)) 
                    && model.Visa.Type == v.Type 
                    && !allVisasSameType.Where(e => e.VisaId == v.VisaId).Any(v => v.DocType == (int)DocumentType.Criminal) 
                    && !model.VisasNotRequireCriminal.Any(i => i.VisaId == v.VisaId) && model.Visa.Type != (int)VisaType.Tourist)
                {
                    model.VisasNotRequireCriminal.Add(v);
                }

                if (/*!model.VisaDocs.Any(v => v.DocumentType == ((int)DocumentType.Ticket))*/
                    //&& 
                    model.Visa.Type == v.Type
                    && !allVisasSameType.Where(e => e.VisaId == v.VisaId).Any(v => v.DocType == (int)DocumentType.Ticket)
                    && !model.VisasNotRequireAviaTickets.Any(i => i.VisaId == v.VisaId) && model.Visa.Type != (int)VisaType.Tourist)
                {
                    model.VisasNotRequireAviaTickets.Add(v);
                }

                if (/*!model.VisaDocs.Any(v => v.DocumentType == ((int)DocumentType.PlaceOfStay))
                    &&*/ model.Visa.Type == v.Type
                    && !allVisasSameType.Where(e => e.VisaId == v.VisaId).Any(v => v.DocType == (int)DocumentType.PlaceOfStay)
                    && !model.VisasNotRequireContract.Any(i => i.VisaId == v.VisaId) && model.Visa.Type != (int)VisaType.Tourist)
                {
                    model.VisasNotRequireContract.Add(v);
                }

                if (!model.VisaDocs.Any(v => v.DocumentType == ((int)DocumentType.FinanceProof))
                    && model.Visa.Type == v.Type
                    && !allVisasSameType.Where(e => e.VisaId == v.VisaId).Any(v => v.DocType == (int)DocumentType.FinanceProof)
                    && !model.VisasNotRequireFinanceProof.Any(i => i.VisaId == v.VisaId) && model.Visa.Type != (int)VisaType.Tourist)
                {
                    model.VisasNotRequireFinanceProof.Add(v);
                }
            }         

            return View("Visa", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
