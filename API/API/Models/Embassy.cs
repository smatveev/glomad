using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class Embassy
    {
        public int Id { get; set; }
        
        [Required]
        public City City { get; set; }        

        [Required]
        public Country Country { get; set; }
        public string CountryIata { get; set; }

        //public virtual

        public string Address { get; set; }
        public string WorkingHours { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SpecialConditions { get; set; }
        public string ProcessingTime { get; set; }
        public string Url { get; set; }
        
        public string ApplicationFormUrl { get; set; }
        public string EappointmentUrl { get; set; }

        [Required]
        public Country OriginalCountry { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte Complexity { get; set; }
        public bool PermanentResidency { get; set; }
        public bool ProvidesСitizenship { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public byte IssueDeadlines { get; set; }
        public string CityIataCode { get; set; }
    }
}
