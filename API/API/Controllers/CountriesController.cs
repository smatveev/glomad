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
    public class CountriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountriesController(AppDbContext context)
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
                                 DevelopmentLevel = c.DevelopmentLevel
                             }).Take(10);

            if (countries.Count() == 0) return NotFound();

            return Ok(countries);
        }

        [HttpGet("GetById")]
        public IActionResult Country(int id)
        {
            var res = _context.Country.FirstOrDefault(m => m.Id == id);

            if (res == null) return NotFound();

            return Ok(res);
        }

        [HttpGet("GetCovidInfo")]
        public IActionResult GetCovidInfo(int countryId)
        {
            var q = _context.Country.FirstOrDefault(c => c.Id == countryId).CovidRestrictions;
            return q != null ? Ok(q) : NotFound();
        }

        [HttpPost("Feedback")]
        public IActionResult CreateFeedback([FromBody] CreateFeedback createFeedback)
        {
            Feedback feedback = new()
            {
                Country = _context.Country.First(c => c.Id == createFeedback.CountryId),
                Email = createFeedback.Email,
                IsNotify = createFeedback.IsNotify,
                Username = createFeedback.Username
            };

            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
