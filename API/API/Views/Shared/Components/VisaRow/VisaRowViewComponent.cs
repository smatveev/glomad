using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Views.Shared.ViewComponents
{
    public class VisaRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(VisaSearchResult visa)
        {
            return View("RowCountry", visa);
        }
    }    
}
