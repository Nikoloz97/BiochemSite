using Microsoft.AspNetCore.Mvc;

namespace BiochemSite
{
    public class FlashCardDataStore : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
