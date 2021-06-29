using API.Models;
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
    public class VisasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<VisaSearchResult> Get(VisaSearch search)
        {
            List<VisaSearchResult> noEntryVisas = (from c in _context.NoVisaEntry
                                                   select new VisaSearchResult
                                                   {
                                                       Description = c.Description,
                                                       VisaName = "Non Entry"
                                                   }).ToList();

            if (noEntryVisas.Count() == 0) return NotFound();

            return Ok(noEntryVisas);
        }
    }
}
