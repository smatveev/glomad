using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glomad.net.Data
{
    public class Route
    {
        private static readonly string[] Names = new[]
        {
            "Russia", "USA", "UK", "Thai"
        };





        public List<Country> Countries { get; set; }

        public Route()
        {
            //var rng = new Random();
            //Countries = Enumerable.Range(1, 10).Select(index => new Country
            //{
            //    Name = Names[rng.Next(Names.Length)],
            //    TempC = rng.Next(-20, 55),
            //    Season = Seasons[rng.Next(Seasons.Length)],
            //    Image = Images[rng.Next(Images.Length)] + ".jpg"
            //}).ToList();

        }
    }

    public class PreparedRoutes
    {
        public List<Route> GetRoutes()
        {
            var countries = new Country().PreparedCountries();            
            var result = new List<Route>();
            result.Add(new Route()
            {
                Countries = countries
            });

            return result;
        }
    }
}
