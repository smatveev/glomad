using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace API.Views.Shared.ViewComponents
{
    public class NoVisaCountriesViewComponent : ViewComponent
    {
        public class NoVisaCountriesModel
        {
            public List<CountryFreeEntry> countries { get; set; }
            public string myCountry { get; set; }
        }

        private readonly AppDbContext _context;

        public NoVisaCountriesViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<CountryFreeEntry> countries)
        {
            var model = new NoVisaCountriesModel();
            string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();

            var res = (from ne in _context.NoVisaEntry
                       join co in _context.Country on ne.CountryPassport.Id equals co.Id
                       where ne.CountryPassport.Name.ToLower() == myCountry.ToLower() 
                        && (ne.IsVisaRequired == false || ne.IsEVisaAvailable == true)
                       select new CountryFreeEntry
                       {
                           Details = ne.Description,
                           Id = ne.CountryDestination.Id,
                           Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                           Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                           EVisaAvailable = ne.IsEVisaAvailable,
                           IsVisaRequired = ne.IsVisaRequired,
                           Duration = ne.Duration,
                       }).ToList();

            model.countries = res;
            //model.myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();
            model.myCountry = myCountry;
            return View(model);
        }
    }    
}
