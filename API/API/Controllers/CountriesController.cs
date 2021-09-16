using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountriesApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Country> Get()
        {
            var countries = (from c in _context.Country
                             select new
                             { id = c.Id,
                                 name = c.Name,
                                 PhotoName = c.PhotoName,
                                 DevelopmentLevel = c.DevelopmentLevel,
                                 Capital = c.CapitalCode
                             });

            if (countries.Count() == 0) return NotFound();

            return Ok(countries);
        }

        [HttpGet("GetCountryEmbassies")]
        public ActionResult<Country> GetCountryEmbassies(string country)
        {
            var q = (from e in _context.Embassy
                     join c in _context.Country
                     on e.Country.Id equals c.Id
                     where e.OriginalCountry.Name == country
                     select new { 
                         Id = e.Id,
                         Country = c.Name,
                         City = e.City.Name,
                         Iata = c.ISOalpha2
                     });

            if (q.Count() == 0) return NotFound();

            return Ok(q);
        }

        [HttpGet("GetById")]
        public IActionResult Country(int id)
        {
            var res = _context.Country.FirstOrDefault(m => m.Id == id);

            if (res == null) return NotFound();

            return Ok(res);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var res = _context.Country.FirstOrDefault(m => m.Name == name);

            if (res == null) return NotFound();

            return Ok(res);
        }

        [HttpGet("GetUpdatedCountries")]
        public IActionResult GetUpdatedCountries()
        {
            var res = (from c in _context.Country where c.UpdateDate != null
                             select new
                             {
                                 id = c.Id,
                                 name = c.Name,
                                 iata = c.ISOalpha2,
                                 updateDate = c.UpdateDate
                             });
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("GetCovidInfo")]
        public IActionResult GetCovidInfo(string name)
        {
            var q = _context.Country.FirstOrDefault(c => c.Name == name).CovidRestrictions;
            return q != null ? Ok(q) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateFeedback(CreateFeedback createFeedback)
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
            
            return Ok();
        }

        [HttpPost("SelectPrice")]
        public IActionResult SelectPrice([FromBody] SelectPlan plan)
        {
            Helpers.EmailSender.Send(plan);
            return Ok();
        }
    }
}
