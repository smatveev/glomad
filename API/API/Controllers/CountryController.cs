using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                                   Iata = c.ISOalpha2
                               }).ToList();

            model.Visas = (from co in _context.Visa
                           where co.Country.Id == model.Country.Id
                           select new VisaSearchResult
                           {
                               Id = co.Id,
                               Description = co.Description,
                               VisaName = co.Name,
                               IsExdendable = co.IsExtendable,
                               Duration = co.Duration
                           }).ToList();

            return View(model);
        }
    }
}
