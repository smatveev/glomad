using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class NoVisaEntry
    {
        public int Id { get; set; }

        [Required]
        public Country CountryPassport {get; set;}

        [Required]
        public Country CountryDestination { get; set; }

        public string Description { get; set; }
        public short Duration { get; set; }
        public string EVisaUrl { get; set; }
    }
}
