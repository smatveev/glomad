﻿using API.Helpers;
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
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = await client.GetAsync("https://freegeoip.app/json/" + HttpContext.Connection.RemoteIpAddress.ToString());
            //response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();

            GeoIp geoIP = await new GeoIp(HttpContext).GetAsync(); //JsonConvert.DeserializeObject<GeoIp>(responseBody);

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
