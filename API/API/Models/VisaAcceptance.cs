using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class VisaAcceptance
    {
        public int ID { get; set; }
        [Required]
        public VisaEmbassy Visa { get; set; }
        [Required]
        public Country Country { get; set; }
        public string CountryIata { get; set; }
    }
}
