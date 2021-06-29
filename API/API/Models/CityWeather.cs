using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glomad.Models
{
    public class CityWeather
    {
        public int Id { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public float Temp { get; set; }
        public string Icon { get; set; }
    }
}
