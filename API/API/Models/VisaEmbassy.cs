using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class VisaEmbassy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Embassy Embassy { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public bool OnArrival { get; set; }
        public Country Country { get; set; }
        public string CountryIata { get; set; }
        public short Duration { get; set; }
        public Visa Visa { get; set; }
    }
}
