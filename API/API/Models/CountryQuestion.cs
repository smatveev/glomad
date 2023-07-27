using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CountryQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        public Country Country { get; set; }
        public string Answer { get; set; }
        [Required]
        public DateTime? UpdateDate { get; set; }
        [Required]
        public Country Citizen { get; set; }
    }
}
