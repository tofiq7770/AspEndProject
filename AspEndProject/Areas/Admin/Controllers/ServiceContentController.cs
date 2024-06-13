using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Areas.Admin.Controllers
{
    public class ServiceContentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
