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

    

//    public enum Vaccines
//    {
//'Oxford–AstraZeneca' => 1, 
//   'Pfizer–BioNTech' => 2, 
//    'Janssen' => 3, 
//    'Moderna' => 4, 
//    'Sinopharm BIBP' =>5, 
//     'Sputnik V' => 6, 
//     'CoronaVac' => 7,
//     'Covaxin' => 8,
//     'Sputnik Light' => 9, 
//     'Convidecia' => 10, 
//     'Sinopharm WIBP' => 11, 
//     'Abdala' => 12, 
//     'EpiVacCorona' => 13, 
//     'Zifivax' => 14, 
//     'Soberana' => 15,
//     'QazCovid-in' => 16, 
//     'Novavax' => 17,
//     'CoviVac' => 18, 
//     'Minhai' => 19,
//     'COVIran Barekat' => 20,
//     'Chinese Academy of Medical Sciences' => 21, 
//     'Medigen' => 22,
//     'ZyCoV-D' => 23, 
//     'FAKHRAVAC' => 24, 
//     'COVAX-19' => 25 
//    }
}
