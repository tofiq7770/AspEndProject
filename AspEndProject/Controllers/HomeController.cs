using AspEndProject.Services.Interface;
using AspEndProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Products = await _productService.GetAllAsync(),
                Categories = await _categoryService.GetAllCategories()
            };

            return View(model);
        }
    }
}
