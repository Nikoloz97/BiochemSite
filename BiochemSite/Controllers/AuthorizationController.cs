using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
