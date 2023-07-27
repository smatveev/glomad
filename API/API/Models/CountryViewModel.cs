using API.Helpers;
using Glomad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CountryViewModel
    {
        public CountryViewModel() {}
        public Country Country { get; set; }
        public string Covid { get; set; }
        public List<EmbassyVM> Embassies { get; set; }
        public List<VisaSearchResult> Visas { get; set; }
        public HeaderViewModel Header { get; set; }
    }

    public struct EmbassyVM {
        public int Id { get; set; }
        public string Country { get; set;}
        public string City { get; set; }
        public string Iata { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Embassy Embassy { get; set; }
    }

    public struct UpdatedCountries
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Iata { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public struct CountryFreeEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iata { get; set; }       
        public string Details { get; set; }
        public bool EVisaAvailable { get; set; }
        public bool IsVisaRequired { get; set; }
        public short Duration { get; set; }
        public string EVisaUrl { get; set; }
    }

    public struct PopularCountriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iata { get; set; }
        public string Reason { get; set; }
    }

    public struct FreeEntry
    {
        public List<CountryFreeEntry> countries { get; set; }
        public HeaderViewModel header { get; set; }
    }

    public struct EmbassiesPage
    {
        public Country Country { get; set; }
        public List<EmbassyVM> Embassies { get; set; }
        public HeaderViewModel Header { get; set; }
    }

    public struct CovidPage
    {
        public string Covid { get; set; }
        public HeaderViewModel Header { get; set; }
        public Country Country { get; set; }
        public List<int> ApprovedVaccines { get; set; }
        public Dictionary<int, byte> Restrictions { get; set; } // RestrictionId and its Levels
        public AmadeusTravelRestrictions AmadeusTravelRestrictions { get; set; }
        public List<string> BannedCountries { get; set; }
        public string HomeCountry { get; set; }
    }

    public struct IndexPage
    {
        public List<VisaSearchResult> Visas { get; set; }
        public HeaderViewModel Header { get; set; }
        public Country Country { get; set; }
        public Country HomeCountry { get; set; }
        public NoVisaEntry NoVisaEntry { get; set; }

        public List<Country> NextCountries { get; set; }
    }

    public struct FaqPage
    {
        public List<CountryQuestion> questions { get; set; }
        public HeaderViewModel header { get; set; }
        public string CountryCapitalCode { get; set; }
    }
}
