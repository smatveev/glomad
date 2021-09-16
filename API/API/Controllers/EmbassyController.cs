using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class EmbassyController : Controller
    {
        private readonly AppDbContext _context;

        public EmbassyController(AppDbContext context)
        {
            _context = context;
        }

        [Route("{country}/Embassy/{id}")]
        public IActionResult Index(string country, int id)
        {
            EmbassyViewModel model = new EmbassyViewModel();
            model.Country = country;
            model.EmbassyWithCountry = (from e in _context.Embassy
                                        join c in _context.Country
                                        on e.Country.Id equals c.Id
                                        where e.Id == id
                                        select new EmbassyWithCountry { 
                                            Embassy = e,
                                            Country = c.Name
                                        }).FirstOrDefault();
            model.Docs = _context.VisaDoc.Where(m => m.Embassy.Id == id).ToList();
            model.VisaDetails = (from v in _context.Visa
                                 join ve in _context.VisaEmbassy
                                 on v.Id equals ve.Visa.Id
                                 where ve.Embassy.Id == id
                                 select new VisaDetails
                                 {
                                     Price = ve.Price,
                                     Currency = ve.Currency,
                                     Duration = ve.Duration,
                                     IsExtendable = v.IsExtendable,
                                     Description = v.Description,
                                     Name = v.Name
                                 }).ToList();
            return View(model);
        }
    }
}
