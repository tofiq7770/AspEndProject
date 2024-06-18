using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
