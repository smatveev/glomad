using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Feedback
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        public bool IsNotify { get; set; }

        [Required]
        public Country Country { get; set; }
    }

    public class CreateFeedback
    {
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        public bool IsNotify { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
