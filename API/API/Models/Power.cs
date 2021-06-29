using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class Power
    {
        public int ID { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public string CountryIata { get; set; }
        public string Voltage { get; set; }
        public string Frequency { get; set; }
        public string Type { get; set; }
    }
}
