using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using System.Data.SqlClient;
using System.Text;

namespace CovidParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connStr = "";
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");

                var config = Configuration.Default.WithDefaultLoader();
                using var context = BrowsingContext.New(config);

                var url = "https://www.emirates.com/english/help/covid-19/travel-requirements-by-destination/";
                using var doc = await context.OpenAsync(url);

                var lis = doc.QuerySelectorAll("li.travel-requirements-list__dropdown-item");

                var total = lis.Length;
                var updated = 0;

                foreach (var li in lis)
                {
                    var country = li.QuerySelector("p.travel-requirements-list__cards-header-title").TextContent;
                    var body = li.QuerySelector("div.travel-requirements-list__item");
                    body.LastElementChild.Remove();
                    //body.QuerySelector("div.travel-requirements-list__travel-requirements-wrapper").Remove();

                   Console.WriteLine("Country = " + country);
                   // Console.WriteLine("html = " + body.InnerHtml);

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append("UPDATE Country SET CovidRestrictions='" + body.InnerHtml.Replace('\'', ' '));
                    strBuilder.Append("' WHERE Name='" + country + "'");


                    string sqlQuery = strBuilder.ToString();
                    using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
                    {
                        try
                        {
                            command.ExecuteNonQuery(); //execute the Query
                            Console.WriteLine("Query Executed.");
                            updated++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                Console.WriteLine("------ Total {0} ----- Updated {1}", total, updated);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            
            Console.ReadLine();
        }
    }
}
