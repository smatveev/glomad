using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class TravelpayoutsResult
    {
        public string iata { get; set; }
        public string name { get; set; }
        public string country_name { get; set; }
        public string coordinates { get; set; }
    }
    public class GeoIp
    {
        private HttpContext _context;

        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int metro_code { get; set; }

        public string country_alpha_2 { get; set; }
        public GeoIp(HttpContext context)
        {
            _context = context;
        }

        public async Task<string> GetMyCountryAsync()
        {
            //string homeCountry = _context.Request.Cookies["homeCountry"];
            string myCountry = _context.Request.Cookies["myCountry"];
            if (string.IsNullOrEmpty(myCountry))
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMonths(1);
                try
                {
                    myCountry = (await GetTravelpayoutsAsync()).country_name;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    myCountry = "Germany";
                }
                _context.Response.Cookies.Append("myCountry", myCountry, option);
            }
            return myCountry;
        }

        private async Task<GeoIp> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var req = "https://api.ip2loc.com/XZEfqZhiVwnfRDX3P2uupnLFqEsY5kho/" + _context.Connection.RemoteIpAddress.ToString();
                var ip = _context.Connection.RemoteIpAddress.MapToIPv6().ToString();
                var response = client.GetAsync("https://api.ip2loc.com/XZEfqZhiVwnfRDX3P2uupnLFqEsY5kho/"+ip+"?include=country_alpha_2").Result; //+ _context.Connection.RemoteIpAddress.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<GeoIp>(responseBody);

                    return JsonConvert.DeserializeObject<GeoIp>(responseBody);
                }
                return null;
            }
        }

        private async Task<TravelpayoutsResult> GetTravelpayoutsAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var req = $"https://www.travelpayouts.com/whereami?locale=en&ip=77.179.51.238"; //{_context.Connection.RemoteIpAddress}
                var response = client.GetAsync(req).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<TravelpayoutsResult>(responseBody);
                    return json;
                }
                return null;
            }
        }
    }
}
