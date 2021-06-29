using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public string ISOalpha2 { get; set; }
        public string ISOalpha3 { get; set; }
        public short ISOnumric { get; set; }
        public string DevelopmentLevel { get; set; }
        public string PhotoName { get; set; }
        public string CovidRestrictions { get; set; }
    }
}
