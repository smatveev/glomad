using Glomad.Models;
using System.Collections.Generic;

namespace API.Models
{
    public class PassportViewModel
    {
        public Country Country { get; set; }
        public List<CountryFreeEntry> CountryFreeEntry { get; set; }
        public List<CountryFreeEntry> CountryEvisa { get; set; }
        public List<PopularCountriesViewModel> PopularCountries { get; set; }
        public HeaderViewModel Header { get; set; }
    }
}
