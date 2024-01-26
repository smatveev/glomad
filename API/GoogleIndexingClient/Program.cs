// See https://aka.ms/new-console-template for more information
using Google.Apis.Auth.OAuth2;
using Google.Apis.Indexing.v3;
using Newtonsoft.Json;

class Program
{
    private const string EndPoint = "https://indexing.googleapis.com/v3/urlNotifications:publish";
    private const int UrlsPerAccount = 200;

    static async Task Main()
    {
        Console.WriteLine("Hello, World!");


        // Check if CSV file exists
        if (!File.Exists("data.csv"))
        {
            Console.WriteLine("Error: data.csv file not found!");
            return;
        }
    }

        private static string SetupHttpClient(string jsonKeyFile)
    {
        GoogleCredential credential;
        using (var stream = new FileStream(jsonKeyFile, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(IndexingService.Scope.Indexing);
        }

        var token = credential.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;
        return token;
    }

    private static async Task IndexUrl(string accessToken, List<string> urls)
    {
        using (var httpClient = new HttpClient())
        {
            var successfulUrls = 0;
            var error429Count = 0;
            var otherErrorsCount = 0;

            foreach (var url in urls)
            {
                var content = new
                {
                    url = url.Trim(),
                    type = "URL_UPDATED"
                };

                for (int retry = 0; retry < 3; retry++)
                {
                    try
                    {
                        var jsonString = JsonConvert.SerializeObject(content);
                        var stringContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                        using (var response = await httpClient.PostAsync(EndPoint, stringContent))
                        {
                            var responseText = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                successfulUrls++;
                            }
                            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                error429Count++;
                            }
                            else
                            {
                                otherErrorsCount++;
                            }
                        }
                    }
                    catch
                    {
                        await Task.Delay(2000); // Wait for 2 seconds before retrying
                        continue;
                    }
                }
            }

            Console.WriteLine($"\nTotal URLs Tried: {urls.Count}");
            Console.WriteLine($"Successful URLs: {successfulUrls}");
            Console.WriteLine($"URLs with Error 429: {error429Count}");
        }
    }
}

class UrlData
{
    public string URL { get; set; }
}