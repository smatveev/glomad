using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Views.Shared.ViewComponents
{
    public class VisaRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(VisaSearchResult visa)
        {
            if (HttpContext.Request.Path.Equals("/")
                || HttpContext.Request.Path.Value.Contains("api/")
                || HttpContext.Request.Path.Value.Contains("visa-"))
            {
                return View(visa);
            }
            return View("RowCountry", visa);
        }
    }    
}
