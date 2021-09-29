using Glomad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EmbassyViewModel
    {
        public EmbassyViewModel() { }

        public List<VisaDoc> Docs { get; set; }
        public List<VisaDetails> VisaDetails { get; set; }
        public string Country { get; set; }
        public EmbassyWithCountryAndCity EmbassyWithCountry { get; set; }
    }

    public struct EmbassyWithCountryAndCity
    {
        public Embassy Embassy { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    public struct VisaDetails
    {
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public short Duration { get; set; }
        public bool IsExtendable { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
