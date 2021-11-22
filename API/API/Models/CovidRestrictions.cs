using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CovidRestrictions
    {
        public int Id { get; set; }
        [Required]
        public Country Country { get; set; }
        public CovidRestriction Restriction { get; set; }
        public byte Level { get; set; }
    }

    public enum CovidRestriction
    {
        One,
        Two,
        Three
    }
}
