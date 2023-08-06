using API.Helpers;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public class MenuModel
        {
            public List<UpdatedCountries> countries { get; set; }
        }

        public MenuViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //string myCountry = await new GeoIp(HttpContext).GetMyCountryAsync();


            //List<UpdatedCountries> countries = (from c in _context.Country
            //                              select new UpdatedCountries
            //                              {
            //                                  Id = c.Id,
            //                                  Name = c.Name,
            //                                  Iata = c.ISOalpha3,
            //                                  UpdateDate = c.UpdateDate.Value
            //                              }).ToList();

            var list = new SelectList(_context.Country, "CapitalCode", "Name");

            return View(model: list);
        }
    }
}
