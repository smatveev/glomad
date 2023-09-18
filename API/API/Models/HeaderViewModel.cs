using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public struct HeaderViewModel
    {
        public string Text { get; set; }
        public string CountryName { get; set; }

        public string HomeCountryName { get; set; }
        public string VisaName { get; set; }
        public string LastUpdate { get; set; }
        public DateTime LastModifiedHeader { get; set; }
    }
}
