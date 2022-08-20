using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Glomad.Models;

namespace API.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public Visa Visa { get; set; }

        [Required]
        public Embassy Embassy { get; set; }
        public byte Simplicity { get; set; }
        public byte Waiting { get; set; }
        public byte Loyalty { get; set; }
        public bool IsObtained { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public ushort Likes { get; set; }
    }

    public class ReviewCreate
    {
        public int VisaId { get; set; }

        public int EmbassyId { get; set; }
        public byte Simplicity { get; set; }
        public byte Waiting { get; set; }
        public byte Loyalty { get; set; }
        public bool IsObtained { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
    }
}
