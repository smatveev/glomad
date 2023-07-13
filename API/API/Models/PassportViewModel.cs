using Glomad.Models;
using System.Collections.Generic;

namespace API.Models
{
    public class PassportViewModel
    {
        public Country Country { get; set; }
        public List<NoVisaEntry> NoVisaCountries { get; set; }
        public HeaderViewModel Header { get; set; }
    }
}
