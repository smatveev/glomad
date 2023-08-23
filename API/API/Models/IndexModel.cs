using Glomad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class IndexModel
    {
        public IndexModel() { 
        }

        [BindProperty]
        public int Passport { get; set; }
        public string PassportCountryName { get; set; }
        public string PassportCapitalCode { get; set; }

        public int To { get; set; }
        public string ToCountryName { get; set; }
        public string ToCapitalCode { get; set; }

        public string CovidInfo { get; set; }

        public List<VisaSearchResult> Visas { get; set; }

        public List<VisaSearchResult> TopLastUpdatedVisas { get; set; } = new List<VisaSearchResult>();
        public List<VisaSearchResult> TopAnnouncedVisas { get; set; } = new List<VisaSearchResult>();
        public List<VisaSearchResult> TopViewedVisas { get; set; } = new List<VisaSearchResult>();
        public List<VisaSearchResult> TopLongestVisas { get; set; } = new List<VisaSearchResult>();
        //public IEnumerable<VisaSearchResult> VisasNonEntry { get; set; }
        public List<CountryFreeEntry> FreeCountries { get; set; }
        public NoVisaEntry NoVisaEntry { get; set; }
        public string Query { get; set; }
    }
}
