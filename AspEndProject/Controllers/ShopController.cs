using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels;
using AspEndProject.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService,
                              ICategoryService categoryService,
                              AppDbContext context)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productService.GetAllAsyncAscending();
            Product product = _context.Products.FirstOrDefault();
            List<CategoryVM> categories = await _categoryService.GetAllCategoriesAsc();
            ShopVM datas = new()
            {
                Products = products,
                Product = product,
                Categories = categories
            };

            return View(datas);
        }
        public async Task<IActionResult> ProductDetail(int? Id)
        {
            var data = await _productService.GetByIdAsync((int)Id);

            return View(data);
        }
    }
}
