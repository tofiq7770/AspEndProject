using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
