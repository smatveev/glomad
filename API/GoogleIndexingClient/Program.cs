// See https://aka.ms/new-console-template for more information
using CsvHelper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Indexing.v3;
using Newtonsoft.Json;
using System.Formats.Asn1;

class Program
{
    private const string EndPoint = "https://indexing.googleapis.com/v3/urlNotifications:publish";
    private const int UrlsPerAccount = 200;

    static async Task Main()
    {
        // Check if CSV file exists
        if (!File.Exists("data.csv"))
        {
            Console.WriteLine("Error: data.csv file not found!");
            return;
        }

        // Ask user for the number of accounts
        Console.Write("How many accounts have you created (1-5)? ");
        if (!int.TryParse(Console.ReadLine(), out int numAccounts) || numAccounts < 1 || numAccounts > 5)
        {
            Console.WriteLine("Invalid number of accounts. Please enter a number between 1 and 5.");
            return;
        }

        // Read all URLs from CSV
        List<string> allUrls;
        try
        {
            using (var reader = new StreamReader("data.csv"))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")))
            {
                allUrls = new List<string>(csv.GetRecords<UrlData>().Select(u => u.URL));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading data.csv: {e.Message}");
            return;
        }

        // Process URLs for each account
        //for (int i = 0; i < numAccounts; i++)
        //{
        //    Console.WriteLine($"\nProcessing URLs for Account {i + 1}...");
        //    string jsonKeyFile = $"account{i + 1}.json";

        //    // Check if account JSON file exists
        //    if (!File.Exists(jsonKeyFile))
        //    {
        //        Console.WriteLine($"Error: {jsonKeyFile} not found!");
        //        continue;
        //    }

        //    int startIndex = i * UrlsPerAccount;
        //    int endIndex = startIndex + UrlsPerAccount;
        //    List<string> urlsForAccount = allUrls.GetRange(startIndex, Math.Min(UrlsPerAccount, allUrls.Count - startIndex));

        //    string accessToken = SetupHttpClient(jsonKeyFile);
        //    await IndexUrl(accessToken, urlsForAccount);
        //}
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