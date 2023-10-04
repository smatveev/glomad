using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Glomad.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iata { get; set; }
        public string TimeZone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int CountryId { get; set; }
        public string CountryIata { get; set; }


        [Display(Name = "Nomad score")]
        public int NomadScore { get; set; }
        [Display(Name = "Internet speed")]
        public int InternetSpeed { get; set; }
        public string PhotoName { get; set; }

        public ICollection<Embassy> Embassies { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostOfLiving { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostOfRent { get; set; }
        public sbyte Temperature { get; set; }
        public int AirQuality { get; set; }
        public byte Safety {get; set; }
        public byte Fun { get; set; }
        public double StartupScore { get; set; }
    }

    public class CityViewModel
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
        [Display(Name = "Nomad score")]
        public int NomadScore { get; set; }
        [Display(Name = "Internet speed")]
        public int InternetSpeed { get; set; }
        public IFormFile Photo { get; set; }
    }

    public class CityListView
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        [Display(Name = "Nomad score")]
        public int NomadScore { get; set; }
        [Display(Name = "Internet speed")]
        public int InternetSpeed { get; set; }
        public string PhotoName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostOfLiving { get; set; }
        public decimal CostOfRent { get; set; }
        public int AirQuality { get; set; }
        public byte Safety { get; set; }
        public byte Fun { get; set; }
        public float Temp { get; set; }
        public double StartupScore { get; set; }
        public string WeatherIcon { get; set; }
        public string Iata { get; set; }

        public string ExcursionName { get; set; }

        public string CountryName { get; set; }
        public List<string> Benefits { get; set; }
        public List<string> Drawbacks { get; set; }
    }

    public class IndexModel
    {
        public IEnumerable<CityListView> Cities { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
    }
}
