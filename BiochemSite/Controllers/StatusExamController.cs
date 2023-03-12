using Microsoft.AspNetCore.Mvc;

namespace BiochemSite.Controllers
{
    [ApiController]
    public class StatusExamController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
