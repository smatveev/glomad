using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Views.Shared.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await Get();
            return View(items);
        }

        private Task<List<string>> Get()
        {
            
            return _context.Country.Where(c => Helpers.Countries.Prepared.Contains(c.Id)).Select(c => c.Name).ToListAsync();
        }
    }    
}
