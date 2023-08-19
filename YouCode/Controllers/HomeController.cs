using Microsoft.AspNetCore.Mvc;

namespace YouCode.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}