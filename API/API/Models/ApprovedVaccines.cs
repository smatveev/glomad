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
        public int VaccineId { get; set; }

        public static Dictionary<int, string> Vaccines = new Dictionary<int, string>(){
            { 1, "Oxford–AstraZeneca" },
            { 2, "Pfizer–BioNTech" },
            { 3, "Janssen" },
            { 4, "Moderna" },
            { 5, "Sinopharm BIBP" },
            { 6, "Sputnik V" },
            { 7, "CoronaVac" },
            { 8, "Covaxin" },
            { 9, "Sputnik Light" },
            { 10, "Convidecia" },
            { 11, "Sinopharm WIBP" },
            { 12, "Abdala" },
            { 13, "EpiVacCorona" },
            { 14, "Zifivax" },
            { 15, "Soberana" },
            { 16, "QazCovid-in" },
            { 17, "Novavax" },
            { 18, "CoviVac" },
            { 19, "Minhai" },
            { 20, "COVIran Barekat" },
            { 21, "Chinese Academy of Medical Sciences" },
            { 22, "Medigen" },
            { 23, "ZyCoV-D" },
            { 24, "FAKHRAVAC" },
            { 25, "COVAX-19" }
        };
    }
}
