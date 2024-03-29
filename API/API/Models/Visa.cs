﻿using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public record VisaSearchCriteria
    {
        public static Dictionary<string, KeyValuePair<string, int>> Criterias = new Dictionary<string, KeyValuePair<string, int>>()
        {
            { "0", new KeyValuePair<string, int>("💳 Visa type", 0) },
            { "for-nomads", new KeyValuePair<string, int>("Digital Nomad Visa", 0) },
            { "for-tourist", new KeyValuePair<string, int>("Tourist", 0) },
            { "for-startup", new KeyValuePair<string, int>("Startup", 0) },
            { "for-business", new KeyValuePair<string, int>("Business", 0) },
            { "for-student", new KeyValuePair<string, int>("Studens", 0) },
            { "for-work", new KeyValuePair<string, int>("W", 0) },

            { "1", new KeyValuePair<string, int>("⏳ Duration", 1) },
            { "short-stay", new KeyValuePair<string, int>("One month", 1) },
            { "middle-stay", new KeyValuePair<string, int>("2+ months", 1) },
            { "long-stay", new KeyValuePair<string, int>("6+ months", 1) },
            { "for-expats", new KeyValuePair<string, int>("1+ year", 1) },

            { "2", new KeyValuePair<string, int>("🤑 Monthly income", 2) },
            { "low-income", new KeyValuePair<string, int>("< $1500", 2) },
            { "middle-income", new KeyValuePair<string, int>("< $4000", 2) },
            { "high-income", new KeyValuePair<string, int>("< $10000", 2) },

            { "3", new KeyValuePair<string, int>("🤑 Benefites", 3) },
            { "are-extendable", new KeyValuePair<string, int>("Extendable", 3) },
            { "not-renewed", new KeyValuePair<string, int>("Not renewed", 3) },
            { "announced", new KeyValuePair<string, int>("Announced", 3) },

            { "4", new KeyValuePair<string, int>("💸 Taxes", 4) },
            { "tax-free", new KeyValuePair<string, int>("Tax free", 4) },
        };
    }

    public class Visa
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public Country Country { get; set; }
        public short Duration { get; set; }
        public bool IsExtendable { get; set; }
        public string Description { get; set; }

        public ushort Type { get; set; }
        public float CostOfProgramm { get; set; }

        public string CostCurrency { get; set; }
        public float Income { get; set; }

        public string ProgramUrl { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActual { get; set; }
        public bool IsAnnounced { get; set; }
        public bool IsFreeVisa { get; set; }

        public byte? TaxSizeUp { get; set; }
        public byte? TaxSizeTo { get; set; }
        public string TaxDescription { get; set; }

        public byte? ProcessingTimeUp { get; set; }
        public byte? ProcessingTimeTo { get; set; }
        public ulong ViewCounter { get; set; }
        public string NameRu { get; set; }
        public string DescriptionRu { get; set; }
    }

    public enum VisaType
    {
        [Description("Startup")]
        Startup,
        [Description("Work")]
        Work,
        [Description("Tourist")]
        Tourist,
        [Description("Digital Nomad")]
        Nomad,
        [Description("Student")]
        Student,
        [Description("Invest")]
        Invest,
        [Description("Business")]
        Business,
        [Description("Retreat")]
        Retreat

    }

    public static class VisaTypes
    {
        public static Dictionary<ushort, string> Types = new Dictionary<ushort, string>()
        {
            { 0, "Startup Visa"},
            { 1, "Work Permit Visa"},
            { 2, "Tourist Visa"},
            { 3, "Digital Nomad Visa"},
            { 4, "Student Visa"},
            { 5, "Invest Visa"},
            { 6, "Business Visa"},
            { 7, "Retirement Visa"},
            { 8, "Retreat"}
        };
    }


    public class VisaSearch
    {
        public int PassportId { get; set; }
        public int CurrentCountryId { get; set; }
        public int DestinationId { get; set; }
    }

    public class VisaSearchResult
    {
        public int? Id { get; set; }
        public string VisaName { get; set; }
        public string Description { get; set; }
        public bool IsExdendable{ get; set; }
        public short Duration { get; set; }
        public List<Review> Reviews { get; set; }
        public string CountryName { get; set; }
        public string Type { get; set; }
        public ushort TypeId { get; set; }
        public float Income { get; set; }
        public string Cost{ get; set; }
        public float CostNum { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsAnnounced { get; set; }
        public byte? TaxSizeTo { get; set; }
    }

    public struct SameVisasOtherCountries
    {
        public string Country { get; set; }
        public string VisaName { get; set; }
        public int VisaId { get; set; }
        public float Income { get; set; }
        public ushort DocType { get; set; }
        public short Duration { get; set; }
        public ushort Type { get;set; }
        public int CountryId { get; set; }
    }

    public class VisaPage
    {
        public Visa Visa { get; set; }
        public List<Review> Reviews { get; set; }
        public List<VisaSearchResult> Visas { get; set; }
        public List<EmbassyVM> Embassies { get; set; }
        public List<VisaDoc> VisaDocs { get; set; }
        public List<SameVisasOtherCountries> SameVisasOtherCountries { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> YearLongVisas { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> CheapVisas { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> AnalogVisa { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> VisasNotRequireCriminal { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> VisasNotRequireAviaTickets { get; set; } = new List<SameVisasOtherCountries>();
        public List<SameVisasOtherCountries> VisasNotRequireContract { get; set; } = new List<SameVisasOtherCountries>(); 
        public List<SameVisasOtherCountries> VisasNotRequireFinanceProof { get; set; } = new List<SameVisasOtherCountries>();
    }
}
