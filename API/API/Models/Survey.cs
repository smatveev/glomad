using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Glomad.Models
{
    public class Survey
    {
        public int Id { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public int Fun { get; set; }
        [Required]
        public byte Internet { get; set; }
        [Required]
        public byte Safety { get; set; }
        [Required]
        public byte Friendly { get; set; }
        [Required]
        public byte StartupScore { get; set; }
        public decimal? CostOfLiving { get; set; }
        public byte? StayDuration { get; set; }
        [Required]
        public bool WantToComeBack { get; set; }
        public DateTime? DateComeBack { get; set; }
        [StringLength(1000)]
        public string Difficulties { get; set; }
        [StringLength(1000)]
        public string Benefits { get; set; }
        [StringLength(1000)]
        public string Drawbacks { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public User User { get; set; }
    }
}
