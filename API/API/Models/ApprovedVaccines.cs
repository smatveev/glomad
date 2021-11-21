using Glomad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ApprovedVaccines
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Vaccines VaccineId { get; set; }
    }

    public enum Vaccines
    {
        SputnikV,
        SputnikLite,
        Autumn,
        Winter
    }
}
