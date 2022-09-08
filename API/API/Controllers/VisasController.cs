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
