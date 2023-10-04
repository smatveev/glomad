using API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class VisaEmbassy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Embassy Embassy { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        //[DataType("DECIMAL(19.4)")]
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public bool OnArrival { get; set; }
        public Country Country { get; set; }
        public string CountryIata { get; set; }
        public short Duration { get; set; }
        public Visa Visa { get; set; }
    }
}
