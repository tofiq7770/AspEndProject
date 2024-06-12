using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
