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
        partial class GeoIP
        {
            public string ip { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public string region_code { get; set; }
            public string region_name { get; set; }
            public string city { get; set; }
            public string zip_code { get; set; }
            public string time_zone { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public int metro_code { get; set; }
        }

        public DirectionsViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("https://freegeoip.app/json/" + HttpContext.Connection.RemoteIpAddress.ToString());
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            GeoIP geoIP = JsonConvert.DeserializeObject<GeoIP>(responseBody);

            DirectionsModel model = new DirectionsModel();

            model.directions = _context.Country
                .Where(c => Helpers.Countries.Prepared.Contains(c.Id))
                .Where(c => c.ISOalpha2.ToLower() != geoIP.country_code.ToLower())
                .Select(c => new { c.Name, c.Id })
                .ToDictionary(c => c.Id, c => c.Name);

            model.from = _context.Country.Where(c => c.ISOalpha2 == geoIP.country_code)
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
