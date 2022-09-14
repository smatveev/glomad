using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Views.Shared.ViewComponents
{
    public class BookingViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BookingViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();
            var hc = _context.Country.Where(c => c.Name.ToLower() == myCountry.ToLower()).FirstOrDefault();
            string myCountryCC = hc.CapitalCode;

            return View(model: myCountryCC);
        }
    }    
}
