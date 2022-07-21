using Glomad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbassiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmbassiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var q = (from e in _context.Embassy
                     join c in _context.Country
                     on e.Country.Id equals c.Id
                     where e.Id == id
                     select new {
                        Embassy = e,
                        Country = c.Name
                     }).FirstOrDefault();

            if (q == null) return NotFound();

            return Ok(q);
        }

        [HttpGet("DocsById")]
        public IActionResult GetDocsById(int id)
        {
            //var res = _context.VisaDoc.Where(m => m.Embassy.Id == id).ToList();
            return NotFound();

            //if (res == null) return NotFound();
            //return Ok(res);
        }

        [HttpGet("VisasById")]
        public IActionResult GetVisasById(int id)
        {
            var q = (from v in _context.Visa
                     join ve in _context.VisaEmbassy
                     on v.Id equals ve.Visa.Id
                     where ve.Embassy.Id == id
                     select new
                     {
                         Price = ve.Price,
                         Currency = ve.Currency,
                         Duration = ve.Duration,
                         IsExtendable = v.IsExtendable,
                         Description = v.Description,
                         Name = v.Name
                     }).ToList();

            if (q == null) return NotFound();

            return Ok(q);
        }
    }
}
