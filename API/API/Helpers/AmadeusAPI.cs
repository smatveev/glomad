using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AmadeusAPI
    {
        private string apiKey;
        private string apiSecret;
        private string bearerToken;
        private HttpClient http;

        public AmadeusAPI(IConfiguration config, IHttpClientFactory httpFactory)
        {
            apiKey = config.GetValue<string>("AmadeusAPI:APIKey");
            apiSecret = config.GetValue<string>("AmadeusAPI:APISecret");
            http = httpFactory.CreateClient("AmadeusAPI");
        }

        public async Task<AmadeusTravelRestrictions> GetTravelRestrictions(string countryCode)
        {
            var message = new HttpRequestMessage(HttpMethod.Get,
                $"/v1/duty-of-care/diseases/covid19-area-report?countryCode={countryCode}");

            ConfigBearerTokenHeader();
            var response = await http.SendAsync(message);
            using var stream = await response.Content.ReadAsStreamAsync();
            var res = await JsonSerializer.DeserializeAsync<AmadeusTravelRestrictions>(stream);
            return res;
        }

        public async Task ConnectOAuth()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/v1/security/oauth2/token");
            message.Content = new StringContent(
                $"grant_type=client_credentials&client_id={apiKey}&client_secret={apiSecret}",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            );

            var results = await http.SendAsync(message);
            await using var stream = await results.Content.ReadAsStreamAsync();
            var oauthResults = await JsonSerializer.DeserializeAsync<OAuthResults>(stream);

            bearerToken = oauthResults.access_token;
        }

        private void ConfigBearerTokenHeader()
        {
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
        }

        private class OAuthResults
        {
            public string access_token { get; set; }
        }
    }

    public class AmadeusTravelRestrictions
    {
        public class Links
        {
            public string self { get; set; }
        }

        public class Area
        {
            public string name { get; set; }
            public string iataCode { get; set; }
            public string areaType { get; set; }
        }

        public class DiseaseInfection
        {
            public string date { get; set; }
            public string level { get; set; }
            public double rate { get; set; }
            public string infectionMapLink { get; set; }
        }

        public class DiseaseCases
        {
            public string date { get; set; }
            public int deaths { get; set; }
            public int confirmed { get; set; }
        }

        public class DataSources
        {
            public string governmentSiteLink { get; set; }
        }

        public class AreaRestriction
        {
            public string date { get; set; }
            public string text { get; set; }
            public string restrictionType { get; set; }
        }

        public class Transportation
        {
            public string date { get; set; }
            public string text { get; set; }
            public string transportationType { get; set; }
            public string isBanned { get; set; }
            public string throughDate { get; set; }
        }

        public class DeclarationDocuments
        {
            public string date { get; set; }
            public string text { get; set; }
            public string documentRequired { get; set; }
        }

        public class BannedArea
        {
            public string iataCode { get; set; }
            public string areaType { get; set; }
        }

        public class BorderBan
        {
            public string borderType { get; set; }
            public string status { get; set; }
        }

        public class Entry
        {
            public string date { get; set; }
            public string text { get; set; }
            public string ban { get; set; }
            public string throughDate { get; set; }
            public string rules { get; set; }
            public string exemptions { get; set; }
            public List<BannedArea> bannedArea { get; set; }
            public List<BorderBan> borderBan { get; set; }
        }

        public class ValidityPeriod
        {
            public string delay { get; set; }
            public string referenceDateType { get; set; }
        }

        public class DiseaseTesting
        {
            public string date { get; set; }
            public string text { get; set; }
            public string isRequired { get; set; }
            public string when { get; set; }
            public string requirement { get; set; }
            public string rules { get; set; }
            public string testType { get; set; }
            public int minimumAge { get; set; }
            public ValidityPeriod validityPeriod { get; set; }
        }

        public class TracingApplication
        {
            public string date { get; set; }
            public string text { get; set; }
            public string isRequired { get; set; }
        }

        public class QuarantineModality
        {
            public string date { get; set; }
            public string text { get; set; }
            public string eligiblePerson { get; set; }
            public string rules { get; set; }
        }

        public class Mask
        {
            public string date { get; set; }
            public string text { get; set; }
            public string isRequired { get; set; }
        }

        public class Exit
        {
            public string date { get; set; }
            public string text { get; set; }
            public string specialRequirements { get; set; }
            public string rulesLink { get; set; }
            public string isBanned { get; set; }
        }

        public class OtherRestriction
        {
        }

        public class DiseaseVaccination
        {
            public string date { get; set; }
            public string isRequired { get; set; }
            public string referenceLink { get; set; }
            public List<string> acceptedCertificates { get; set; }
            public List<string> qualifiedVaccines { get; set; }
            public string policy { get; set; }
        }

        public class AreaAccessRestriction
        {
            public Transportation transportation { get; set; }
            public DeclarationDocuments declarationDocuments { get; set; }
            public Entry entry { get; set; }
            public DiseaseTesting diseaseTesting { get; set; }
            public TracingApplication tracingApplication { get; set; }
            public QuarantineModality quarantineModality { get; set; }
            public Mask mask { get; set; }
            public Exit exit { get; set; }
            public OtherRestriction otherRestriction { get; set; }
            public DiseaseVaccination diseaseVaccination { get; set; }
        }

        public class AreaPolicy
        {
            public string date { get; set; }
            public string text { get; set; }
            public string status { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string referenceLink { get; set; }
        }

        public class RelatedArea
        {
            public List<string> methods { get; set; }
            public string rel { get; set; }
        }

        public class AreaVaccinated
        {
            public string date { get; set; }
            public string vaccinationDoseStatus { get; set; }
            public double percentage { get; set; }
        }

        public class Data
        {
            public Area area { get; set; }
            public string summary { get; set; }
            public string diseaseRiskLevel { get; set; }
            public DiseaseInfection diseaseInfection { get; set; }
            public DiseaseCases diseaseCases { get; set; }
            public string hotspots { get; set; }
            public DataSources dataSources { get; set; }
            public List<AreaRestriction> areaRestrictions { get; set; }
            public AreaAccessRestriction areaAccessRestriction { get; set; }
            public AreaPolicy areaPolicy { get; set; }
            public List<RelatedArea> relatedArea { get; set; }
            public List<AreaVaccinated> areaVaccinated { get; set; }
            public string type { get; set; }
        }

        public Data data { get; set; }
    }
}