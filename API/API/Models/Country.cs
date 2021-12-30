using API.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsQuarantine { get; set; }
        public string Quarantine { get; set; }
        public string AcceptableVaccinations { get; set; }
        public bool IsPcrRequired{ get; set; }
        public string PCR { get; set; }
        public DateTime? UpdateDate { get; set; }
        [StringLength(20)]
        public string CapitalCode { get; set; }
        public short LetNoEntryVisaCount { get; set; }
        public short LetEVisaCount { get; set; }
        public short LetVisaCount { get; set; }
        public string Citizen { get; set; }
        public string AmadeusTravelRestrictions { get; set; }
    }

    public class CountryVM
    {
        public CountryVM() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
        public string CapitalCode { get; set; }
    }
}
