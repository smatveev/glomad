using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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
                       where c.UpdateDate != null
                       select new UpdatedCountries
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Iata = c.ISOalpha3,
                           UpdateDate = c.UpdateDate.Value
                       }).ToList();
            ViewBag.Countries = res;
            return View("Countries");
        }

        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        public IActionResult Index(int CitizenshipId, int To = 220)
        {
            var mo = new API.Models.IndexModel();
            mo.CitizenshipId = CitizenshipId;

            if(CitizenshipId > 0)
            {
                mo.VisasNonEntry = (from co in _context.NoVisaEntry
                                    where co.CountryDestination.Id == To && co.CountryPassport.Id == CitizenshipId
                                    select new VisaSearchResult
                                    {
                                        Id = co.Id,
                                        Description = co.Description,
                                        VisaName = "No Entry Visa"
                                    });
            }
            if(To > 0)
            {
                mo.CovidInfo = _context.Country.FirstOrDefault(c => c.Id == To).CovidRestrictions; // Thai
                mo.Visas = (from co in _context.Visa
                            where co.Country.Id == To
                            select new VisaSearchResult
                            {
                                Id = co.Id,
                                Description = co.Description,
                                VisaName = co.Name,
                                IsExdendable = co.IsExtendable,
                                Duration = co.Duration
                            });
            }
            
            

            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            return View(mo);
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
