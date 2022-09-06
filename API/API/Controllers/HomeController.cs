using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Net.Http;
using API.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Countries")]
        public IActionResult Index()
        {
            List<UpdatedCountries> res = (from c in _context.Country
                       select new UpdatedCountries
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Iata = c.ISOalpha3,
                           UpdateDate = c.UpdateDate.Value
                       }).ToList();

            ViewBag.Countries = res;
            ViewBag.VisaOptions = _context.NoVisaEntry.Count();
            ViewBag.Embassies = _context.Embassy.Count();
            return View("Countries");
        }

        [Route("")]
        //[Route("Search")]
        //[Route("Search/{route}")]
        public IActionResult Index(int Passport, int To)
        {
            var mo = new Models.IndexModel();
            mo.Passport = Passport;
            mo.To = To;

            //TODO: refactor this shit
            mo.Visas = VisaHelper.GetVisasFromLink("", _context);

            if (To > 0)
            {
                mo.ToCountryName = _context.Country.FirstOrDefault(c => c.Id == mo.To).Name;
                mo.ToCapitalCode = _context.Country.FirstOrDefault(c => c.Id == mo.To).CapitalCode;

                mo.CovidInfo = _context.Country.FirstOrDefault(c => c.Id == To).CovidRestrictions;

                mo.Visas = (from co in _context.Visa
                            where co.Country.Id == To
                            select new VisaSearchResult
                            {
                                Id = co.Id,
                                Description = co.Description,
                                VisaName = co.Name,
                                IsExdendable = co.IsExtendable,
                                Duration = co.Duration,
                                CountryName = mo.ToCountryName,
                                Reviews = _context.Review.Where(r => r.Visa.Id == co.Id).ToList(),
                                Type = ((VisaType)co.Type).ToString(),
                                Income = co.Income,
                                Cost = $"{co.CostOfProgramm} {co.CostCurrency}"
                            }).ToList();
            }

            if (mo.Passport > 0)
            {
                var HomeCountry = _context.Country.Where(c => c.Id == mo.Passport).FirstOrDefault();

                mo.NoVisaEntry = _context.NoVisaEntry
                    .Where(i => i.CountryDestination.Id == To && i.CountryPassport.Id == mo.Passport).FirstOrDefault();

                mo.PassportCapitalCode = HomeCountry.CapitalCode;
                mo.PassportCountryName = HomeCountry.Name;

                mo.FreeCountries = (from ne in _context.NoVisaEntry
                                    join co in _context.Country on ne.CountryPassport.Id equals co.Id
                                    where ne.CountryPassport.Id == mo.Passport && ne.IsVisaRequired == false
                                    select new CountryFreeEntry
                                    {
                                        Details = ne.Description,
                                        Id = ne.CountryDestination.Id,
                                        Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                                        Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                                        EVisaAvailable = ne.IsEVisaAvailable,
                                        IsVisaRequired = ne.IsVisaRequired
                                    }).ToList();
            }

            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            ViewBag.ToCountries = ViewBag.Countries;

            return View(mo);
        }

        [Route("visa-{query}")]
        //[Route("Search")]
        //[Route("Search/{route}")]
        public IActionResult Index(string query)
        {
            var mo = new Models.IndexModel();
            mo.Query = query;
            mo.Visas = VisaHelper.GetVisasFromLink(query, _context);

            return View(mo);
        }

        [HttpPost("SelectPrice")]
        public IActionResult SelectPrice([FromBody] SelectPlan plan)
        {
            Helpers.EmailSender.Send(plan);
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] CreateFeedback createFeedback)
        {
            try
            {
                Feedback feedback = new()
                {
                    Country = _context.Country.First(c => c.Id == createFeedback.CountryId),
                    Email = createFeedback.Email ?? "none",
                    IsNotify = createFeedback.IsNotify,
                    Username = createFeedback.Username ?? "none"
                };

                _context.Feedback.Add(feedback);
                _context.SaveChanges();
                Helpers.EmailSender.SendFeedback(createFeedback);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("ImprovePage")]
        [HttpPost]
        public IActionResult ImprovePage([FromBody] ImprovePage improvePage)
        {
            try
            {
                ImprovePage improve = new()
                {
                    Name = improvePage.Name,
                    Email = improvePage.Email,
                    Message = improvePage.Message,
                    Link = improvePage.Link
                };

                Helpers.EmailSender.SendImprovePage(improve);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
