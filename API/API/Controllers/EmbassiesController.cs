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
            var res = _context.Embassy.FirstOrDefault(m => m.Id == id);

            if (res == null) return NotFound();

            return Ok(res);
        }

        [HttpGet("DocsById")]
        public IActionResult GetDocsById(int id)
        {
            var res = _context.VisaDoc.FirstOrDefault(m => m.Embassy.Id == id);

            if (res == null) return NotFound();

            return Ok(res);
        }
    }
}
