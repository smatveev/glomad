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
        public ulong ViewCounter { get; set; }
        public byte? TaxFreeMounthsCount { get; set; }
        public string Summary { get; set; }
        public string DriverLicense { get; set; }
        public string DriverRules { get; set; }
        public string PopularPlaces { get; set; }
        public string BestSeason { get; set; }
        public string CurrencyDescription { get; set; }
        public string Etiquette { get; set; }
        public string Vaccinations { get; set; }
        public string Transport { get; set; }
        public string Safety { get; set; }
        public string EmrgencyNumbers { get; set; }
        public string Internet { get; set; }
        public string MobileOperators { get; set; }
        //public BestMonths? BestMonths { get; set; }
        //public СountriesNearby? СountriesNearby { get; set; }
        public byte? SafetyLevel{ get; set; }

        public string NextCountries { get; set; }
        public string PopularCountries { get; set; }

    }

    //public class BestMonths
    //{
    //    public string Months { get; set; }
    //}
    //public class СountriesNearby
    //{
    //    public string Countries { get; set; }
    //}

    public class CountryVM
    {
        public CountryVM() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
        public string CapitalCode { get; set; }
    }
}
