using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Glomad.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Glomad.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public CitiesController(AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        //[HttpGet]
        //public IActionResult GetCity(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var city = _context.City
        //        .FirstOrDefault(m => m.Id == id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    return new JsonResult(city);
        //}


        [HttpGet]
        public IActionResult GetCity(string term)
        {
            if (term == null)
            {
                return NotFound();
            }

            var city = (from c in _context.City
                        where c.Name.StartsWith(term)
                        select new { id = c.Id, value = c.Name }).ToList();

            if (city == null)
            {
                return NotFound();
            }

            return new JsonResult(city);
        }
        
        [HttpGet]
        [Route("api/GetRatingCitiesNames")]
        public JsonResult GetRatingCitiesNames()
        {
            var cities = from c in _context.City
                               where c.NomadScore > 0
                               select new { id = c.Id, name = c.Name };

            return Json(cities.ToList());
        }

        [HttpPost]
        [Route("api/UpdateCitiesWeather")]
        public IActionResult UpdateCitiesWeather(string cityId, string temp, string icon)
        {
            float Temp = float.Parse(temp);
            int CityId = Convert.ToInt32(cityId);

            var weather = _context.CityWeather.SingleOrDefault(c => c.City.Id == CityId) ?? new CityWeather();

            weather.City = _context.City.SingleOrDefault(c => c.Id == CityId);
            weather.Temp = Temp;
            weather.Icon = icon;

            _context.AddOrUpdate(weather);
            _context.SaveChanges();

            return Ok();
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
