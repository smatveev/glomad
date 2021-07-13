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
            var res = _context.VisaDoc.Where(m => m.Embassy.Id == id).ToList();

            if (res == null) return NotFound();

            return Ok(res);
        }
    }
}
