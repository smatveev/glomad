using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glomad.Models
{
    public class ClimateData
    {
        public int Id { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public string CountryIata { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public string CityCode { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
