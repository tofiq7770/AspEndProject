using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
