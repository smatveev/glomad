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

            model.CountryFreeEntry = (from ne in _context.NoVisaEntry
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

            HeaderViewModel header = new HeaderViewModel();
            header.Text = $"Detail information for cizitens of {countryName}";
            header.CountryName = countryName;
            model.Header = header;

            return View(model);
        }
    }
}
