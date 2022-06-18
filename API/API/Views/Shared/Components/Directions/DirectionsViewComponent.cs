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
    public class DirectionsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public DirectionsViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DirectionsModel model = new DirectionsModel();
            string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync(); //JsonConvert.DeserializeObject<GeoIp>(responseBody);
            model.directions = _context.Country
                .Where(c => Helpers.Countries.Prepared.Contains(c.Id))
                .Where(c => c.ISOalpha2.ToLower() != myCountry)
                .Select(c => new { c.Name, c.Id })
                .ToDictionary(c => c.Id, c => c.Name);

            model.from = _context.Country.Where(c => c.ISOalpha2 == myCountry)
                .Select(c => new KeyValuePair<int, string>(c.Id, c.Name)).FirstOrDefault();

            return View(model);
        }
    }

    public class DirectionsModel
    {
        public Dictionary<int, string> directions { get; set; }
        public KeyValuePair<int, string> from { get; set; }

    }
}
