using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
