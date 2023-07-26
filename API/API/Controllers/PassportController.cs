﻿using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class PassportController : Controller
    {
        private readonly AppDbContext _context;

        public PassportController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Passport/{country}")]
        public IActionResult Index(string country)
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

            HeaderViewModel header = new HeaderViewModel();
            header.Text = $"Detail information for cizitens of {countryName}";
            header.CountryName = countryName;
            model.Header = header;


            var popCountries = model.Country.PopularCountries?.Split(',')?.Select(int.Parse)?.ToList();

            //var countryIds = model.Country.PopularCountries.Split(',');
            //var intCountryIds = new int[countryIds.Length];

            //for (int i = 0; i < countryIds.Length; i++)
            //{
            //    intCountryIds[i] = int.Parse(countryIds[i]);
            //}

            model.PopularCountries = (from pc in _context.PopularCountries join c in _context.Country on pc.Country.Id equals c.Id
                        where popCountries.Contains(pc.Country.Id)
                        select c).Distinct().ToList();

            return View(model);
        }
    }
}
