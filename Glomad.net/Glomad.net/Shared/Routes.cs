using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glomad.net.Shared
{
    class Routes
    {
        public List<Country> countries;

        public Routes()
        {
            countries = new List<Country>
            {
                new Country {Name = "UK"},
                new Country {Name = "US"},
                new Country {Name = "Russia"}
            };
        }
    }

    class Country
    {
        public string Name { get; set; }
    }
}
