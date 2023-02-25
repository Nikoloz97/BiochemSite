using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    public class StatusExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
