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
    }
}
