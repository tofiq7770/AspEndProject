using AspEndProject.DAL;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService,
                              ICategoryService categoryService,
                              AppDbContext context)
        {

            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            HomeVM model = new()
            {

                Products = await _productService.GetAllAsyncAscending(),
                Categories = await _categoryService.GetAllCategoriesAsc(),
                Sliders = await _context.Sliders.ToListAsync(),
                SliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(),
                FactContents = await _context.FactContents.ToListAsync(),
                ServiceContents = await _context.ServiceContents.ToListAsync(),
                Fresh = await _context.Freshs.FirstOrDefaultAsync(),
                Features = await _context.Features.ToListAsync(),
                Review = await _context.Reviews.Include(m => m.AppUser).ToListAsync(),
            };

            return View(model);
        }
    }
}
