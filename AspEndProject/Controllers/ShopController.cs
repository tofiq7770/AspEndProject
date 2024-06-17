using AspEndProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _productService.GetAllAsync();

            return View(datas);
        }
        public async Task<IActionResult> ProductDetail(int? Id)
        {
            var data = await _productService.GetByIdAsync((int)Id);

            return View(data);
        }
    }
}
