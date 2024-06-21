using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels;
using AspEndProject.ViewModels.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopController(IProductService productService,
                              ICategoryService categoryService,
                              AppDbContext context,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IHttpContextAccessor httpContextAccessor

                              )
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
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
            List<Product> products = await _productService.GetAllAsyncAscending();
            Product product = await _productService.GetByIdAsync((int)Id);
            List<CategoryVM> categories = await _categoryService.GetAllCategoriesAsc();

            AppUser existUser = new();

            if (User.Identity.IsAuthenticated)
                existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.AppUserId = existUser.Id;

            ShopVM datas = new()
            {
                Products = products,
                Product = product,
                Categories = categories
            };

            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string message)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { redirectUrl = Url.Action("Login", "Account") });

            AppUser existUser = new();
            existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            Review newReview = new()
            {
                Image = existUser.Image,
                Message = message,
                ProductId = productId,
                AppUserId = existUser.Id
            };

            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();

            return PartialView("_ReviewPartial", newReview);
        }



        public async Task<IActionResult> Sorting(string sort)
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();

            switch (sort)
            {
                case "Old to New":
                    products = products.OrderBy(m => m.Id);
                    break;
                case "Cheap to Expensive":
                    products = products.OrderBy(m => m.Price);
                    break;
                case "Expensive to Cheap":
                    products = products.OrderByDescending(m => m.Price);
                    break;
            }

            ShopVM model = new() { Products = products };

            return PartialView("_ProductsFilterPartial", model);
        }


        public async Task<IActionResult> Search(string searchText)
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();

            products = searchText != null
                ? products.Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
                : products.Take(6);

            ShopVM model = new() { Products = products };

            return PartialView("_ProductsFilterPartial", model);
        }

        public async Task<IActionResult> CategoryFilter(int id)
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();

            ShopVM model = new() { Products = products.Where(m => m.CategoryId == id) };

            return PartialView("_ProductsFilterPartial", model);
        }

        public async Task<IActionResult> PriceFilter(int price)
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();
            IEnumerable<Product> filteredProducts = price > 0
                ? products.Where(p => p.Price <= price)
                : products;
            ShopVM model = new()
            {
                Products = filteredProducts.ToList()
            };
            return PartialView("_ProductsFilterPartial", model);
        }

    }
}
