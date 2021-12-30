using System;

namespace API.Models
{
    public class AmadeusApi
    {
        public int Id { get; set; }
        public DateTime? LastCall { get; set; }
        public int CallsLimit { get; set; }
    }
}
