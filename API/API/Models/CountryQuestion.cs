using Glomad.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CountryQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
