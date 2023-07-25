using API.Models;
using Glomad.Models;
using Humanizer;
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

        public static List<string> TypeFilters = new List<string>()
        {
             "for-startup",
             "for-work",
             "for-tourist",
             "for-nomads",
             "for-student",
             "for-invest",
             "for-business"

        };

        public static List<VisaSearchResult> GetVisasFromLink(string q, AppDbContext _context)
        {
            var result = new List<VisaSearchResult>();

            if (string.IsNullOrEmpty(q))
            {
                result = (from co in _context.Visa
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
                              //Type = ((VisaType)co.Type).GetEnumDescription(),
                              Type = VisaTypes.Types[co.Type],
                              TypeId = co.Type,
                              Income = co.Income,
                              Cost = $"{co.CostOfProgramm} {co.CostCurrency}",
                              CostNum = co.CostOfProgramm,
                              UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : c.UpdateDate.Value
                          }).Take(10).OrderByDescending(r => r.Reviews.Count).ToList();

                return result;
            }


            //(co => q.Contains("for-nomads") ? co.Type == (int)VisaType.Nomad : true) ||
            //                (q.Contains("for-tourist") ? co.Type == (int)VisaType.Tourist : true) ||
            //                (q.Contains("for-startup") ? co.Type == (int)VisaType.Startup : true) ||
            //                (q.Contains("for-business") ? co.Type == (int)VisaType.Business : true) ||
            //                (q.Contains("for-student") ? co.Type == (int)VisaType.Student : true) ||
            //                (q.Contains("for-work") ? co.Type == (int)VisaType.Work : true)

            var filters = new List<int>();
            
            for (int i = 0; i < TypeFilters.Count; i++) {

                if (q.Contains(TypeFilters[i])) {
                    filters.Add(i);
                }
            }
            if (filters.Count > 0)
            {
                result = (from co in _context.Visa
                          join c in _context.Country on co.Country.Id equals c.Id
                          where filters.Contains(co.Type)
                          select new VisaSearchResult
                          {
                              Id = co.Id,
                              Description = co.Description,
                              VisaName = co.Name,
                              IsExdendable = co.IsExtendable,
                              Duration = co.Duration,
                              CountryName = c.Name,
                              Reviews = _context.Review.Where(r => r.Visa.Id == co.Id).ToList(),
                              Type = VisaTypes.Types[co.Type],
                              TypeId = co.Type,
                              Income = co.Income,
                              Cost = $"{co.CostOfProgramm} {co.CostCurrency}",
                              CostNum = co.CostOfProgramm,
                              UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : c.UpdateDate.Value
                          }).ToList();
            }
            else {
                result = (from co in _context.Visa
                          join c in _context.Country on co.Country.Id equals c.Id
                         // where test.Contains(co.Type)
                          select new VisaSearchResult
                          {
                              Id = co.Id,
                              Description = co.Description,
                              VisaName = co.Name,
                              IsExdendable = co.IsExtendable,
                              Duration = co.Duration,
                              CountryName = c.Name,
                              Reviews = _context.Review.Where(r => r.Visa.Id == co.Id).ToList(),
                              Type = VisaTypes.Types[co.Type],
                              TypeId = co.Type,
                              Income = co.Income,
                              Cost = $"{co.CostOfProgramm} {co.CostCurrency}",
                              CostNum = co.CostOfProgramm,
                              UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : c.UpdateDate.Value
                          }).ToList();

            }


            if (q.Contains("low-income")) result.RemoveAll(v => v.Income > 1500);
            if (q.Contains("middle-income")) result.RemoveAll(v => v.Income > 4000);
            if (q.Contains("high-income")) result.RemoveAll(v => v.Income > 10000);

            if (q.Contains("short-stay")) { 
                result.RemoveAll(v => v.Duration > 30); 
            
            }
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
