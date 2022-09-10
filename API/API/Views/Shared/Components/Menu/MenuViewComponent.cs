using API.Helpers;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Views.Shared.Components.Directions
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public MenuViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();
            //DirectionsModel model = new DirectionsModel();
            //string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync(); //JsonConvert.DeserializeObject<GeoIp>(responseBody);
            //model.directions = _context.Country
            //    .Where(c => Countries.Prepared.Contains(c.Id))
            //    //.Where(c => c.ISOalpha2.ToLower() != myCountry)
            //    .Where(c => c.Name.ToLower() != myCountry.ToLower())
            //    .Select(c => new { c.Name, c.Id })
            //    .ToDictionary(c => c.Id, c => c.Name);

            //model.from = _context.Country.Where(c => c.Name.ToLower() == myCountry.ToLower())
            //    .Select(c => new KeyValuePair<int, string>(c.Id, c.Name)).FirstOrDefault();

            return View(model: myCountry);
        }
    }
}
