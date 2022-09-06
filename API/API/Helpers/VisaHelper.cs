using API.Models;
using Glomad.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Helpers
{
    public class VisaHelper
    {
        public static List<string> VisaFilters = new List<string>()
        {
            "-for-nomads",
            "-for-tourist",
            "-for-startup"
        };

        public static List<VisaSearchResult> GetVisasFromLink(string q, AppDbContext _context)
        {
            var result = new List<VisaSearchResult>();

            var Visas = (from co in _context.Visa
                         join c in _context.Country on co.Country.Id equals c.Id
                         select new VisaSearchResult
                         {
                             Id = co.Id,
                             Description = co.Description,
                             VisaName = co.Name,
                             IsExdendable = co.IsExtendable,
                             Duration = co.Duration,
                             CountryName = c.Name,
                             Reviews = _context.Review.Where(r => r.Visa.Id == co.Id).ToList(),
                             Type = ((VisaType)co.Type).ToString(),
                             TypeId = co.Type,
                             Income = co.Income,
                             Cost = $"{co.CostOfProgramm} {co.CostCurrency}",
                             CostNum = co.CostOfProgramm
                         }).ToList();

            

            if (q.Contains("for-nomads")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Nomad));
            else if (q.Contains("for-tourist")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Tourist));
            else if (q.Contains("for-startup")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Startup));
            else if (q.Contains("for-business")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Business));
            else if (q.Contains("for-student")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Student));
            else if (q.Contains("for-work")) result.AddRange(Visas.Where(v => v.TypeId == (int)VisaType.Work));
            else result = new List<VisaSearchResult>(Visas);

            if (q.Contains("low-income")) result.RemoveAll(v => v.Income > 1500);
            if (q.Contains("middle-income")) result.RemoveAll(v => v.Income > 4000);
            if (q.Contains("high-income")) result.RemoveAll(v => v.Income > 10000);

            if (q.Contains("short-stay")) result.RemoveAll(v => v.Duration > 30);
            if (q.Contains("middle-stay")) result.RemoveAll(v => v.Duration < 30 || v.Duration > 180);
            if (q.Contains("long-stay")) result.RemoveAll(v => v.Duration > 365 || v.Duration < 180);
            if (q.Contains("for-expats")) result.RemoveAll(v => v.Duration < 360);

            if (q.Contains("are-extendable")) result.RemoveAll(v => !v.IsExdendable);
            if (q.Contains("not-renewed")) result.RemoveAll(v => v.IsExdendable);

            //if (q.Contains("no-criminal-need")) result.RemoveAll(v => v.);
            //if (q.Contains("no-avia-tickets")) result.RemoveAll(v => v.Duration < 30 && v.Duration > 180);
            //if (q.Contains("no-accomodation-proof")) result.RemoveAll(v => v.Duration > 365 && v.Duration < 180);
            //if (q.Contains("no-finance-proof")) result.RemoveAll(v => v.Duration < 360);
            //if (q.Contains("no-insuranse")) result.RemoveAll(v => v.Duration < 360);

            return result;
        }
    }
}
