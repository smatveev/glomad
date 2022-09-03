using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Views.Shared.ViewComponents
{
    public class VisaSearchResultViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<VisaSearchResult> visas)
        {
            return View(visas);
        }
    }    
}
