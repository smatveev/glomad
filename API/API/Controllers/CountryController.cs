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
            var model = new CountryViewModel();

            model.Country = _context.Country.FirstOrDefault(m => m.Name == country);
            if (model.Country == null)
                return RedirectToAction("Index", "Home");

            model.Covid = _context.Country.FirstOrDefault(c => c.Id == model.Country.Id).CovidRestrictions;

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

            return View(model);
        }

        [Route("{country}/NoEntry")]
        public IActionResult NoEntry(string country)
        {
            return RedirectToActionPermanent(actionName: "NoVisaEntry", controllerName: country);
        }

        [Route("{country}/NoVisaEntry")]
        public IActionResult NoVisaEntry(string country)
        {
            ViewBag.CountryName = country.FirstCharToUpper();
            ViewBag.LastUpdates = _context.Country.Where(c => c.Name == country).Select(r => r.UpdateDate.Value).ToString();
            ViewBag.Citizen = _context.Country.Where(c => c.Name == country).Select(r => r.Citizen);

            var model = new List<CountryFreeEntry>();

            model = (from ne in _context.NoVisaEntry
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

            return View("NoVisaEntry", model);
        }

        [Route("{country}/FreeEntry")]
        public IActionResult FreeEntry(string country)
        {
            ViewBag.CountryName = country.FirstCharToUpper();
            ViewBag.Citizen = _context.Country.Where(c => c.Name == country).FirstOrDefault().Citizen;

            var countries = (from ne in _context.NoVisaEntry
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

            return View("FreeEntry", countries);
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
                    IsObtained = reviewCreate.IsObtained
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
