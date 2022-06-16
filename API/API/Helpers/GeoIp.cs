using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Helpers
{
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

        public GeoIp(HttpContext context)
        {
            _context = context;
        }

        public async Task<GeoIp> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var req = "https://api.ipbase.com/v2/info?apikey=CntgTKh7GBimFtEaDuKrjUA7naGd8xJXhcA1IWjI&ip=" + _context.Connection.RemoteIpAddress.ToString();
                var response = client.GetAsync("https://api.ipbase.com/v2/info?apikey=CntgTKh7GBimFtEaDuKrjUA7naGd8xJXhcA1IWjI&ip=" + _context.Connection.RemoteIpAddress.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<GeoIp>(responseBody);
                }
                return null;
            }
        }
    }
}
