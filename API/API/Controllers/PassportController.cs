using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            model.NoVisaCountries = _context.NoVisaEntry
                .Where(i => i.CountryPassport.Id == model.Country.Id).ToList();

            HeaderViewModel header = new HeaderViewModel();
            header.Text = $"Detail information for cizitens of {countryName}";
            header.CountryName = countryName;
            model.Header = header;

            return View(model);
        }
    }
}
