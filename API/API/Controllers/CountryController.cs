﻿using API.Models;
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
                                   Iata = c.ISOalpha3.ToLower()
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

        [Route("{country}/NoEntry")]
        public IActionResult NoEntry(string country)
        {
            ViewBag.CountryName = country;
            var countries = (from ne in _context.NoVisaEntry
                         join co in _context.Country on ne.CountryDestination.Id equals co.Id
                         where co.Name == country
                         select new CountryNoEntry
                         {
                             Details = ne.Description,
                             Id = co.Id,
                             Iata = ne.CountryPassport.ISOalpha3 != null ? ne.CountryPassport.ISOalpha3 : "No data",
                             Name = ne.CountryPassport.Name != null ? ne.CountryPassport.Name : "No data"
                         });

            var model = new Dictionary<string, List<CountryNoEntry>>();
            foreach (var c in countries)
            {
                var key = c.Details.Trim();
                if (model.ContainsKey(key))
                    model[key].Add(c);
                else
                    model.Add(key, new List<CountryNoEntry>() { c });
            }

            return View("NoEntry", model);
        }
    }
}
