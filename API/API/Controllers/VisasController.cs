using API.Helpers;
using API.Models;
using Glomad.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisasController : Controller
    {
        private readonly AppDbContext _context;

        public VisasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetVisas/")]
        [HttpGet("GetVisas/{q}")]
        public IActionResult GetVisas(string q)
        {
            var visas = VisaHelper.GetVisasFromLink(q, _context);
            return ViewComponent("VisaSearchResult", visas);
        }

        [HttpGet("NoVisasEntry/")]
        public IActionResult NoVisasEntry()
        {
            return ViewComponent("NoVisaCountries");
        }

        [HttpGet("BookingW/")]
        public IActionResult BookingW()
        {
            return ViewComponent("Booking", _context);
        }

        [HttpGet("GetNoVisaCountries/")]
        [HttpGet("GetNoVisaCountries/{countryId}")]
        public IActionResult GetNoVisaCountries(int countryId)
        {
            string myCountry = new GeoIp(HttpContext).GetMyCountryAsync(_context).ToString().ToLower();
            var res = (from ne in _context.NoVisaEntry
                            join co in _context.Country on ne.CountryPassport.Id equals co.Id
                            where ne.CountryPassport.Name.ToLower() == myCountry
                            select new CountryFreeEntry
                            {
                                Details = ne.Description,
                                Id = ne.CountryDestination.Id,
                                Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                                Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                                EVisaAvailable = ne.IsEVisaAvailable,
                                IsVisaRequired = ne.IsVisaRequired
                            }).ToList();
            return ViewComponent("NoVisaCountries", res);
        }

        [HttpGet("GetNonEntry")]
        public ActionResult<VisaSearchResult> GetNonEntry(int DestinationId, int PassportId)
        {
            var noEntryVisas = (from co in _context.NoVisaEntry
                                where co.CountryDestination.Id == DestinationId && co.CountryPassport.Id == PassportId
                                select new VisaSearchResult
                                {
                                    Id = co.Id,
                                    Description = co.Description,
                                    VisaName = "No Entry Visa"
                                }).ToList();

            if (noEntryVisas.Count() == 0) return NotFound();

            return Ok(noEntryVisas);
        }

        [HttpGet]
        public ActionResult<VisaSearchResult> Get(string country)
        {
            var q = (from co in _context.Visa
                                where co.Country.Name == country
                                select new VisaSearchResult
                                {
                                    Id = co.Id,
                                    Description = co.Description,
                                    VisaName = co.Name,
                                    IsExdendable = co.IsExtendable,
                                    Duration = co.Duration
                                }).ToList();

            if (q.Count() == 0) return NotFound();

            return Ok(q);
        }
    }
}
