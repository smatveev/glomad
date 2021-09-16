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
    }

    public struct EmbassyVM {
        public int Id { get; set; }
        public string Country { get; set;}
        public string City { get; set; }
        public string Iata { get; set; }
    }
}
