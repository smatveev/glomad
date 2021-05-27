using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.net.Data
{
    public class Country
    {
        private static readonly string[] Images = new[]
        {
            "Bangkok", "Berlin", "Chiang-mai", "Kuala-Lumpur", "Ubud"
        };
        private static readonly string[] Seasons = new[]
{
            "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public string Name { get; set; }
        public int TempC { get; set; }
        public string Season { get; set; }
        public string Image { get; set; }

        public int VisaDays { get; set; }

        public string[] VisaCountry { get; set; }

        public List<Country> PreparedCountries()
        {
            var rng = new Random();
            var result = new List<Country>();

            result.Add(new Country()
            {
                Image = Images[rng.Next(Images.Length)] + ".jpg",
                Name = "Mexico",
                VisaDays = 180,
                Season = "Сезон кргулый год. Можно стартануть в любое время.",// Seasons[rng.Next(Seasons.Length)],
                VisaCountry = new string[] { "Russian Federation", "Turkey", "Ukraine", "Canada", "United States of America" }
            });

            result.Add(new Country()
            {
                Image = Images[rng.Next(Images.Length)] + ".jpg",
                Name = "Колумбия",
                VisaDays = 180,
                Season = "Круглый год. Высокий сезон с декабря по март",// Seasons[rng.Next(Seasons.Length)],
                VisaCountry = new string[] { "Albania", "Andorra", "Antigua and Barbuda", "Argentina", "Australia", "Austria", "Azerbaijan", "United States of America" }
            });

            result.Add(new Country()
            {
                Image = Images[rng.Next(Images.Length)] + ".jpg",
                Name = "Chili",
                VisaDays = 90,
                Season = "КThe warmest season is between October and April and the coldest, from May to September.",// Seasons[rng.Next(Seasons.Length)],
                VisaCountry = new string[] { "Russian Federation", "Turkey", "Ukraine", "Canada", "United States of America" }
            });

            result.Add(new Country()
            {
                Image = Images[rng.Next(Images.Length)] + ".jpg",
                Name = "Brazil",
                VisaDays = 90,
                Season = "Brazil's summer is Dec-Mar, and winter Jun-Sep - but heat and humidity rise the further north you go. Overall, Sep-Oct is the best time to visit Brazil - avoiding major vacation periods.",// Seasons[rng.Next(Seasons.Length)],
                VisaCountry = new string[] { "Russian Federation", "Turkey", "Ukraine", "Canada", "United States of America" }
            });

            return result;
        }

        public string[] GetCountries()
        {
            return new string[] {
            "Andorra",
            "United Arab Emirates",
            "Afghanistan",
            "Antigua and Barbuda",
            "Albania",
            "Armenia",
            "Angola",
            "Argentina",
            "Austria",
            "Australia",
            "Azerbaijan",
            "Bosnia and Herzegovina",
            "Barbados",
            "Bangladesh",
            "Belgium",
            "Burkina Faso",
            "Bulgaria",
            "Bahrain",
            "Burundi",
            "Benin",
            "Brunei Darussalam",
            "Bolivia (Plurinational State of)",
            "Brazil",
            "Bahamas",
            "Bhutan",
            "Botswana",
            "Belarus",
            "Belize",
            "Canada",
            "Democratic Republic of the Congo",
            "Central African Republic",
            "Congo",
            "Switzerland",
            "Côte d'Ivoire",
            "Chile",
            "Cameroon",
            "China",
            "Colombia",
            "Costa Rica",
            "Cuba",
            "Cape Verde",
            "Cyprus",
            "Czech Republic",
            "Germany",
            "Djibouti",
            "Denmark",
            "Dominica",
            "Dominican Republic",
            "Algeria",
            "Ecuador",
            "Estonia",
            "Egypt",
            "Eritrea",
            "Spain",
            "Ethiopia",
            "Finland",
            "Fiji",
            "Micronesia (Federated States of)",
            "France",
            "Gabon",
            "United Kingdom of Great Britain and Northern Ireland",
            "Grenada",
            "Georgia",
            "Ghana",
            "Gambia",
            "Guinea",
            "Equatorial Guinea",
            "Greece",
            "Guatemala",
            "Guinea-Bissau",
            "Guyana",
            "Honduras",
            "Croatia",
            "Haiti",
            "Hungary",
            "Indonesia",
            "Ireland",
            "Israel",
            "India",
            "Iraq",
            "Iran (Islamic Republic of)",
            "Iceland",
            "Italy",
            "Jamaica",
            "Jordan",
            "Japan",
            "Kenya",
            "Kyrgyzstan",
            "Cambodia",
            "Kiribati",
            "Comoros",
            "Saint Kitts and Nevis",
            "Democratic People's Republic of Korea",
            "Republic of Korea",
            "Kuwait",
            "Kazakhstan",
            "Lao People's Democratic Republic",
            "Lebanon",
            "Saint Lucia",
            "Liechtenstein",
            "Sri Lanka",
            "Liberia",
            "Lesotho",
            "Lithuania",
            "Luxembourg",
            "Latvia",
            "Libyan Arab Jamahiriya",
            "Morocco",
            "Monaco",
            "Republic of Moldova",
            "Montenegro",
            "Madagascar",
            "Marshall Islands",
            "The former Yugoslav Republic of Macedonia",
            "Mali",
            "Myanmar",
            "Mongolia",
            "Mauritania",
            "Malta",
            "Mauritius",
            "Maldives",
            "Malawi",
            "Mexico",
            "Malaysia",
            "Mozambique",
            "Namibia",
            "Niger",
            "Nigeria",
            "Nicaragua",
            "Netherlands",
            "Norway",
            "Nepal",
            "Nauru",
            "New Zealand",
            "Oman",
            "Panama",
            "Peru",
            "Papua New Guinea",
            "Philippines",
            "Pakistan",
            "Poland",
            "Portugal",
            "Palau",
            "Paraguay",
            "Qatar",
            "Romania",
            "Serbia",
            "Russian Federation",
            "Rwanda",
            "Saudi Arabia",
            "Solomon Islands",
            "Seychelles",
            "Sudan",
            "Sweden",
            "Singapore",
            "Slovenia",
            "Slovakia",
            "Sierra Leone",
            "San Marino",
            "Senegal",
            "Somalia",
            "Suriname",
            "South Sudan",
            "Sao Tome and Principe",
            "El Salvador",
            "Syrian Arab Republic",
            "Swaziland",
            "Chad",
            "Togo",
            "Thailand",
            "Tajikistan",
            "Timor-Leste",
            "Turkmenistan",
            "Tunisia",
            "Tonga",
            "Turkey",
            "Trinidad and Tobago",
            "Tuvalu",
            "United Republic of Tanzania",
            "Ukraine",
            "Uganda",
            "United States of America",
            "Uruguay",
            "Uzbekistan",
            "Saint Vincent and the Grenadines",
            "Venezuela (Bolivarian Republic of)",
            "Viet Nam",
            "Vanuatu",
            "Samoa",
            "Yemen",
            "South Africa",
            "Zambia",
            "Zimbabwe"
            };
        }
    }
}
