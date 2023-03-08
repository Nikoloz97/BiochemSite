using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    public class FlashCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
