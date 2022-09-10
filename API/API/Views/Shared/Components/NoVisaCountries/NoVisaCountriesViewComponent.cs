using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Views.Shared.ViewComponents
{
    public class NoVisaCountriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<CountryFreeEntry> countries)
        {
            return View(countries);
        }
    }    
}
