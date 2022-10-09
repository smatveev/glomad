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
        public string Text { get; set; }

        [Required]
        public bool IsNotify { get; set; }

        [Required]
        public int CountryId { get; set; }
    }

    public class SelectPlan
    {
        public string Name { get; set; }
        public string Username { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }
        public string Details { get; set; }
    }

    public class ImprovePage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
    }

    public class ReportVisa
    {
        public string Text { get; set; }
        public string Url { get; set; }
    }

    public class ShareExperience
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
    }
}
