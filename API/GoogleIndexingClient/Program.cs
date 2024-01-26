// See https://aka.ms/new-console-template for more information
using Google.Apis.Auth.OAuth2;
using Google.Apis.Indexing.v3;

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


}

class UrlData
{
    public string URL { get; set; }
}