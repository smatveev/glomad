using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Views.Shared.ViewComponents
{
    public class NoVisaCountriesViewComponent : ViewComponent
    {
        public class NoVisaCountriesModel
        {
            public List<CountryFreeEntry> countries { get; set; }
            public string myCountry { get; set; }
        }

        public async Task<IViewComponentResult> InvokeAsync(List<CountryFreeEntry> countries)
        {
            var model = new NoVisaCountriesModel();
            model.countries = countries;
            model.myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();
            return View(model);
        }
    }    
}
