using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CheckOutController(UserManager<AppUser> userManager,
                              AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            AppUser existUser = new();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            else
            {
                existUser = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Basket basket = await _context.Baskets?
                .Include(m => m.BasketProducts)
                .ThenInclude(m => m.Product)
                .ThenInclude(m => m.ProductImages)
                .FirstOrDefaultAsync(m => m.AppUserId == existUser.Id);

            List<BasketListVM> basketListVMs = new();

            if (basket != null)
            {
                foreach (var item in basket.BasketProducts)
                {
                    basketListVMs.Add(new BasketListVM()
                    {
                        Id = item.Id,
                        Image = item.Product.ProductImages.FirstOrDefault(m => m.IsMain)?.Image,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        TotalPrice = item.Product.Price * item.Quantity
                    });
                }
            }
            ViewBag.BasketProduct = basketListVMs;

            return View(basketListVMs);
        }

    }
}
