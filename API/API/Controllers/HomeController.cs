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
using System.IO.Compression;
using System.IO;

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
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><sitemapindex xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            string site = "https://glomad.net";

            var allCountries = (from c in _context.Country
                             select new
                             {
                                 Name = c.Name,
                                 UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();

            foreach (var item in allCountries)
            {
                sb.Append(
                    $"<sitemap><loc>{site}/sitemap/{item.Name}-sitemap.xml.gz</loc></sitemap>");
            }
            sb.Append("</sitemapindex>");

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };
        }

        [Route("/sitemap/{country}-sitemap.csv")]
        public FileResult CountrySitemapAsCvs(string country)
        {
            string fileName = $"{country}-data.csv";            

            string site = "https://glomad.net";

            var csv = new StringBuilder();
            csv.AppendLine("URL,Date");

            var countriesAll = (from c in _context.Country
                                where c.Name.ToLower() != country
                                select new
                                {
                                    Name = c.Name,
                                    UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                }).Distinct().ToList();

            foreach (var to in countriesAll)
            {
                csv.AppendLine(string.Format($"{site}/{country}/{to.Name},"));
            }

            var faqs = (from c in _context.Country
                        join q in _context.CountryQuestion on c.Id equals q.Country.Id
                        where c.Name.ToLower() == country
                        select c.Name).Distinct().ToList();

            foreach (var faq in faqs)
            {
                csv.AppendLine(string.Format($"{site}/{country}/FAQ,"));
            }


            List<VisaSearchResult> visas = (from v in _context.Visa
                                            join c in _context.Country on v.Country.Id equals c.Id
                                            where c.Name.ToLower() == country
                                            select new VisaSearchResult
                                            {
                                                CountryName = c.Name,
                                                Id = v.Id,
                                                UpdateDate = v.UpdateDate.HasValue ? v.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                            }).ToList();

            foreach (var visa in visas)
            {
                csv.AppendLine(string.Format($"{site}/{country}/visa/{visa.Id},"));
            }


            var countrywithembassies = (from c in _context.Country
                                        join e in _context.Embassy on c.Id equals e.Country.Id
                                        where c.Name.ToLower() == country
                                        select new
                                        {
                                            Name = c.Name,
                                            UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                        }).Distinct().ToList();

            foreach (var ce in countrywithembassies)
            {
                csv.AppendLine(string.Format($"{site}/{country}/Embassies,"));               
            }


            var neCountry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryDestination.Id equals co.Id
                             where (!ne.IsVisaRequired || ne.IsEVisaAvailable) && (co.Name.ToLower() == country)
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();
            foreach (var c in neCountry)
            {
                csv.AppendLine(string.Format($"{site}/{country}/NoVisaEntry,"));
            }


            var freeEntry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where ne.IsVisaRequired == false && co.Name.ToLower() == country
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();

            foreach (var c in freeEntry)
            {
                csv.AppendLine(string.Format($"{site}/{country}/FreeEntry,"));
            }

            var passports = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where co.Name.ToLower() == country
                             select new
                             {
                                 co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();

            foreach (var p in passports)
            {
                csv.AppendLine(string.Format($"{site}/{country}/Passport,"));
            }

            var countriesWithSummary = (from c in _context.Country
                                        where (c.Summary != null || c.Summary.Length > 0) && c.Name.ToLower() == country
                                        select new
                                        {
                                            Name = c.Name,
                                            UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                        }).Distinct().ToList();

            foreach (var item in countriesWithSummary)
            {
                csv.AppendLine(string.Format($"{site}/{country}/Auto,"));
                csv.AppendLine(string.Format($"{site}/{country}/Season,"));
                csv.AppendLine(string.Format($"{site}/{country}/Guide,"));
                csv.AppendLine(string.Format($"{site}/{country}/Info,"));
                csv.AppendLine(string.Format($"{site}/{country}/Safety,"));
            }

            byte[] fileBytes = Encoding.ASCII.GetBytes(csv.ToString());
            return File(fileBytes, "text/csv", fileName); // this is the key!
        }

        [Route("/sitemap/{country}-sitemap.xml.gz")]
        public IActionResult CountrySitemap(string country)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?><urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            string site = "https://glomad.net";

            var countriesAll = (from c in _context.Country
                                where c.Name.ToLower() != country
                                select new
                                {
                                    Name = c.Name,
                                    UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                }).Distinct().ToList();

            foreach (var to in countriesAll)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/{to.Name}</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }

            var faqs = (from c in _context.Country
                        join q in _context.CountryQuestion on c.Id equals q.Country.Id
                        where c.Name.ToLower() == country
                        select c.Name).Distinct().ToList();

            foreach (var faq in faqs)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/FAQ</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }


            List<VisaSearchResult> visas = (from v in _context.Visa
                                            join c in _context.Country on v.Country.Id equals c.Id
                                            where c.Name.ToLower() == country
                                            select new VisaSearchResult
                                            {
                                                CountryName = c.Name,
                                                Id = v.Id,
                                                UpdateDate = v.UpdateDate.HasValue ? v.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                            }).ToList();

            foreach (var visa in visas)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/visa/{visa.Id}</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }


            var countrywithembassies = (from c in _context.Country
                                        join e in _context.Embassy on c.Id equals e.Country.Id
                                        where c.Name.ToLower() == country
                                        select new
                                        {
                                            Name = c.Name,
                                            UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                        }).Distinct().ToList();

            foreach (var ce in countrywithembassies)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/Embassies</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }


            var neCountry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryDestination.Id equals co.Id
                             where (!ne.IsVisaRequired || ne.IsEVisaAvailable) && (co.Name.ToLower() == country)
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();
            foreach (var c in neCountry)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/NoVisaEntry</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }


            var freeEntry = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where ne.IsVisaRequired == false && co.Name.ToLower() == country
                             select new
                             {
                                 Name = co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-7)
                             }).Distinct().ToList();

            foreach (var c in freeEntry)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/FreeEntry</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }

            var passports = (from ne in _context.NoVisaEntry
                             join co in _context.Country on ne.CountryPassport.Id equals co.Id
                             where co.Name.ToLower() == country
                             select new
                             {
                                 co.Name,
                                 UpdateDate = co.UpdateDate.HasValue ? co.UpdateDate.Value : DateTime.Now.AddDays(-30)
                             }).Distinct().ToList();

            foreach (var p in passports)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/Passport</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }

            var countriesWithSummary = (from c in _context.Country
                                        where (c.Summary != null || c.Summary.Length > 0) && c.Name.ToLower() == country
                                        select new
                                        {
                                            Name = c.Name,
                                            UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                        }).Distinct().ToList();

            foreach (var item in countriesWithSummary)
            {
                sb.Append(
                    $"<url><loc>{site}/{country}/Auto</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
                sb.Append(
                    $"<url><loc>{site}/{country}/Season</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
                sb.Append(
                    $"<url><loc>{site}/{country}/Guide</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
                sb.Append(
                    $"<url><loc>{site}/{country}/Info</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
                sb.Append(
                    $"<url><loc>{site}/{country}/Safety</loc>" +
                    $"<changefreq>daily</changefreq>" +
                    $"<priority>0.9</priority></url>");
            }
            sb.Append("</urlset>");

            // Compress the XML content
            byte[] compressedData;
            using (var ms = new MemoryStream())
            {
                using (var gz = new GZipStream(ms, CompressionMode.Compress))
                using (var writer = new StreamWriter(gz, Encoding.UTF8))
                {
                    writer.Write(sb.ToString());
                }

                compressedData = ms.ToArray();
            }

            // Set response headers for gzip compression
            Response.Headers.Add("Content-Encoding", "gzip");
            Response.Headers.Add("Vary", "Accept-Encoding");

            return new FileContentResult(compressedData, "application/xml");
        }

        [Route("sitemap2.xml")]
        public IActionResult Sitemap2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8' ?><urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            string site = "https://glomad.net/";

            sb.Append(
                $"<url><loc>{site}</loc>" +
                $"<lastmod>{DateTime.Now.ToString("yyyy-MM-dd")}</lastmod>" +
                $"<changefreq>daily</changefreq>" +
                $"<priority>0.8</priority></url>");

            var countriesAfter100 = _context.Country.Select(c => c.Name).Skip(100).ToList();
            var countriesAll = _context.Country.Select(c => c.Name).ToList();

            try
            {
                foreach (var from in countriesAfter100)
                {
                    foreach (var to in countriesAll)
                    {
                        if (!from.Equals(to))
                        {
                            sb.Append(
                                $"<url><loc>{site}{from}/{to}</loc>" +
                                $"<changefreq>weekly</changefreq>" +
                                $"<priority>1</priority></url>");
                        }
                    }
                }
            }
            catch { }


            var countriesWithSummary = (from c in _context.Country
                                        where c.Summary != null || c.Summary.Length > 0
                                        select new
                                        {
                                            Name = c.Name,
                                            UpdateDate = c.UpdateDate.HasValue ? c.UpdateDate.Value : DateTime.Now.AddDays(-30)
                                        }).Distinct().ToList();
            try
            {
                foreach (var item in countriesWithSummary)
                {
                    sb.Append(
                        $"<url><loc>{site}{item.Name}/Auto</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.5</priority></url>");
                    sb.Append(
                        $"<url><loc>{site}{item.Name}/Season</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.5</priority></url>");
                    sb.Append(
                        $"<url><loc>{site}{item.Name}/Guide</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.5</priority></url>");
                    sb.Append(
                        $"<url><loc>{site}{item.Name}/Info</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.5</priority></url>");
                    sb.Append(
                        $"<url><loc>{site}{item.Name}/Safety</loc>" +
                        $"<lastmod>{item.UpdateDate.ToString("yyyy-MM-dd")}</lastmod>" +
                        $"<changefreq>weekly</changefreq>" +
                        $"<priority>0.5</priority></url>");
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
        }

        [Route("")]
        [Route("citizen-{country}")]
        [Route("visa-{query}")]
        //[Route("Search")]
        //[Route("Search/{route}")]
        public async Task<IActionResult> Index(string country, string query)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            
            var mo = new Models.IndexModel();

            //TODO: refactor this shit
            mo.Visas = VisaHelper.GetVisasFromLink(query, _context);
            mo.Query = query;            

            mo.TopLastUpdatedVisas = VisaHelper.GetTopLastUpdatedVisas(_context);
            mo.TopLongestVisas = VisaHelper.GetTopLongestVisas(_context);
            mo.TopAnnouncedVisas = VisaHelper.GetTopAnnouncedVisas(_context);
            mo.TopViewedVisas = VisaHelper.GetTopViewedVisas(_context);

            ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
            ViewBag.ToCountries = ViewBag.Countries;


            var myCountry = await new GeoIp(HttpContext).GetMyCountryAsync(_context);
            try
            {
                var hc = _context.Country.Where(c => c.Name.ToLower() == myCountry.ToLower()).FirstOrDefault();
                ViewBag.MyCountry = hc.CapitalCode;
            }
            catch(Exception e)
            {                
            }
            

            ViewBag.Citizen = country;

            //sw.Stop();
            //Console.WriteLine("Elapsed = {0}", sw.Elapsed);

            return View(mo);
        }

        //[Route("visa-{query}")]
        ////[Route("Search")]
        ////[Route("Search/{route}")]
        //public IActionResult Index(string query)
        //{
        //    var mo = new Models.IndexModel();
        //    mo.Query = query;
        //    mo.Visas = VisaHelper.GetVisasFromLink(query, _context);

        //    ViewBag.Countries = new SelectList(_context.Country, "Id", "Name");
        //    ViewBag.ToCountries = ViewBag.Countries;

        //    return View(mo);
        //}

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
