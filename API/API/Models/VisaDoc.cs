using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class VisaDoc
    {
        public int Id { get; set; }

        [Required]
        public Embassy Embassy { get; set; }

        public string Text { get; set; }
        public Visa Visa { get; set; }
    }
}
