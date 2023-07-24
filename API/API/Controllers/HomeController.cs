using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using API.Models;
using Glomad.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using API.Helpers;
using System.Threading.Tasks;
using System.Text;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Countries")]
        public IActionResult Index()
        {
            List<UpdatedCountries> res = (from c in _context.Country
                       select new UpdatedCountries
                       {
                           Id = c.Id,
                           Name = c.Name,
                           Iata = c.ISOalpha3,
                           UpdateDate = c.UpdateDate.Value
                       }).ToList();

            ViewBag.Countries = res;
            ViewBag.VisaOptions = _context.NoVisaEntry.Count();
            ViewBag.Embassies = _context.Embassy.Count();
            return View("Countries");
        }

        [Route("sitemap.xml")]
        public IActionResult Sitemap()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8' ?><urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            string site = "https://glomad.net/";

            sb.Append(
                $"<url><loc>{site}</loc>" +
                $"<lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>" +
                $"<changefreq>daily</changefreq>" +
                $"<priority>0.8</priority></url>");


            var countries = (from c in _context.Country
                             join v in _context.Visa
                             on c.Id equals v.Country.Id
                             select new
                             {
                                 Name = c.Name,
                                 UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();

            try {
                foreach (var item in countries)
                {
                    sb.Append(
                        $"<url><loc>{site}{item.Name}</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }


            var faqs = (from c in _context.Country
                             join q in _context.CountryQuestion
                             on c.Id equals q.Country.Id
                             select new
                             {
                                 Name = c.Name,
                                 UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();

            try
            {
                foreach (var country in faqs)
                {
                    sb.Append(
                        $"<url><loc>{site}{country.Name}/FAQ</loc>" +
                        $"<lastmod>{country.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            List<VisaSearchResult> visas = (from v in _context.Visa
                        join c in _context.Country
                        on v.Country.Id equals c.Id
                        //where v.UpdateDate != null
                        select new VisaSearchResult
                        {
                            CountryName = c.Name,
                            Id = v.Id,
                            UpdateDate = v.UpdateDate.HasValue ? v.UpdateDate.Value : DateTime.Now.AddDays(-30)
                        }).ToList();

            try
            {
                foreach (var visa in visas)
                {
                    sb.Append(
                        $"<url><loc>{site}{visa.CountryName}/visa/{visa.Id}</loc>" +
                        $"<lastmod>{visa.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            var countrywithembassies = (from c in _context.Country
                             join e in _context.Embassy
                             on c.Id equals e.Country.Id
                             select new
                             {
                                 Country = c.Name,
                                 UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();
            try
            {
                foreach (var ce in countrywithembassies)
                {
                    sb.Append(
                        $"<url><loc>{site}{ce.Country}/Embassies</loc>" +
                        $"<lastmod>{ce.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            //var embassies = (from e in _context.Embassy
            //                join c in _context.Country
            //                on e.Country.Id equals c.Id
            //                select new
            //                {
            //                    Id = e.Id,
            //                    Country = c.Name,
            //                    UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
            //                }).ToList();

            //try
            //{
            //    foreach (var e in embassies)
            //    {
            //        sb.Append(
            //            $"<url><loc>{site}{e.Country}/Embassy/{e.Id}</loc>" +
            //            $"<lastmod>{e.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
            //            $"<changefreq>weekly</changefreq>" +
            //            $"<priority>0.8</priority></url>");
            //    }
            //}
            //catch { }


            var cCovid = (from c in _context.Country
                             where c.UpdateDate != null
                             select new
                             {
                                 Name = c.Name,
                                 UpdateDate = c.UpdateDate.Value
                             }).ToList();
            try
            {
                foreach (var c in cCovid)
                {
                    sb.Append(
                        $"<url><loc>{site}{c.Name}/Covid</loc>" +
                        $"<lastmod>{c.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            var neCountry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryDestination.Id equals co.Id
                             where !ne.IsVisaRequired || ne.IsEVisaAvailable
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();
            try
            {
                foreach (var c in neCountry)
                {
                    sb.Append(
                        $"<url><loc>{site}{c.Name}/NoVisaEntry</loc>" +
                        $"<lastmod>{c.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            var freeEntry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where ne.IsVisaRequired == false
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();
            try
            {
                foreach (var c in freeEntry)
                {
                    sb.Append(
                        $"<url><loc>{site}{c.Name}/FreeEntry</loc>" +
                        $"<lastmod>{c.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.8</priority></url>");
                }
            }
            catch { }

            sb.Append("</urlset>");

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };

            //return View("Sitemap");
        }

        [Route("")]
        //[Route("Search")]
        //[Route("Search/{route}")]
        public async Task<IActionResult> Index(int Passport, int To)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            var mo = new Models.IndexModel();
            mo.Passport = Passport;
            mo.To = To;

            //TODO: refactor this shit
            mo.Visas = VisaHelper.GetVisasFromLink("", _context);

            if (To > 0)
            {
                mo.ToCountryName = _context.Country.FirstOrDefault(c => c.Id == mo.To).Name;
                mo.ToCapitalCode = _context.Country.FirstOrDefault(c => c.Id == mo.To).CapitalCode;

                mo.CovidInfo = _context.Country.FirstOrDefault(c => c.Id == To).CovidRestrictions;

                mo.Visas = (from co in _context.Visa
                            where co.Country.Id == To
                            select new VisaSearchResult
                            {
                                Id = co.Id,
                                Description = co.Description,
                                VisaName = co.Name,
                                IsExdendable = co.IsExtendable,
                                Duration = co.Duration,
                                CountryName = mo.ToCountryName,
                                Reviews = _context.Review.Where(r => r.Visa.Id == co.Id).ToList(),
                                Type = ((VisaType)co.Type).ToString(),
                                Income = co.Income,
                                Cost = $"{co.CostOfProgramm} {co.CostCurrency}"
                            }).ToList();
            }

            if (mo.Passport > 0)
            {
                var HomeCountry = _context.Country.Where(c => c.Id == mo.Passport).FirstOrDefault();

                mo.NoVisaEntry = _context.NoVisaEntry
                    .Where(i => i.CountryDestination.Id == To && i.CountryPassport.Id == mo.Passport).FirstOrDefault();

                mo.PassportCapitalCode = HomeCountry.CapitalCode;
                mo.PassportCountryName = HomeCountry.Name;

                mo.FreeCountries = (from ne in _context.NoVisaEntry
                                    join co in _context.Country on ne.CountryPassport.Id equals co.Id
                                    where ne.CountryPassport.Id == mo.Passport && ne.IsVisaRequired == false
                                    select new CountryFreeEntry
                                    {
                                        Details = ne.Description,
                                        Id = ne.CountryDestination.Id,
                                        Iata = ne.CountryDestination.ISOalpha3 != null ? ne.CountryDestination.ISOalpha3 : "No data",
                                        Name = ne.CountryDestination.Name != null ? ne.CountryDestination.Name : "No data",
                                        EVisaAvailable = ne.IsEVisaAvailable,
                                        IsVisaRequired = ne.IsVisaRequired
                                    }).ToList();
            }

            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            ViewBag.ToCountries = ViewBag.Countries;

            var country = await new GeoIp(HttpContext).GetMyCountryAsync();
            var hc = _context.Country.Where(c => c.Name.ToLower() == country.ToLower()).FirstOrDefault();
            ViewBag.MyCountry = hc.CapitalCode;

            //sw.Stop();
            //Console.WriteLine("Elapsed = {0}", sw.Elapsed);

            return View(mo);
        }

        [Route("visa-{query}")]
        //[Route("Search")]
        //[Route("Search/{route}")]
        public IActionResult Index(string query)
        {
            var mo = new Models.IndexModel();
            mo.Query = query;
            mo.Visas = VisaHelper.GetVisasFromLink(query, _context);

            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            ViewBag.ToCountries = ViewBag.Countries;

            return View(mo);
        }

        [HttpPost("SelectPrice")]
        public IActionResult SelectPrice([FromBody] SelectPlan plan)
        {
            Helpers.EmailSender.Send(plan);
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] CreateFeedback createFeedback)
        {
            try
            {
                //Feedback feedback = new()
                //{
                //    Country = _context.Country.First(c => c.Id == createFeedback.CountryId),
                //    Email = createFeedback.Email ?? "none",
                //    IsNotify = createFeedback.IsNotify,
                //    Username = createFeedback.Username ?? "none"
                //};

                //_context.Feedback.Add(feedback);
                //_context.SaveChanges();
                Helpers.EmailSender.SendFeedback(createFeedback);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("ImprovePage")]
        [HttpPost]
        public IActionResult ImprovePage([FromBody] ImprovePage improvePage)
        {
            try
            {
                ImprovePage improve = new()
                {
                    Name = improvePage.Name,
                    Email = improvePage.Email,
                    Message = improvePage.Message,
                    Link = improvePage.Link
                };

                Helpers.EmailSender.SendImprovePage(improve);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("SubscribeUpdates")]
        [HttpPost]
        public IActionResult SubscribeUpdates([FromBody] SubscribeUpdates subscribeUpdates)
        {
            try
            {
                SubscribeUpdates subscribe = new()
                {
                    Name = subscribeUpdates.Name,
                    Email = subscribeUpdates.Email,
                    Link = subscribeUpdates.Link
                };

                Helpers.EmailSender.SendSubscribeUpdates(subscribe);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        [Route("ShareExperience")]
        [HttpPost]
        public IActionResult ShareExperience([FromBody] ShareExperience exp)
        {
            try
            {
                ShareExperience E = new()
                {
                    Email = exp.Email,
                    Message = exp.Message,
                    Link = exp.Link
                };

                Helpers.EmailSender.SendShareExperience(E);
            }
            catch (Exception e)
            {
                return Error();
            }

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
