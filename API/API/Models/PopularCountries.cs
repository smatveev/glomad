using System.ComponentModel.DataAnnotations;

namespace Glomad.Models
{
    public class PopularCountries
    {
        public int Id { get; set; }

        public Country Citizenship { get; set; }

        public Country Country { get; set; }
        public string Reason { get; set; }
    }
}