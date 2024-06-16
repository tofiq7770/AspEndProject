using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _context = appDbContext;
            _userManager = userManager;
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

            return View(basketListVMs);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int? productId)
        {
            if (productId is null)
                return BadRequest();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            AppUser existUser = await _userManager.FindByNameAsync(User.Identity.Name);

            Product existProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

            if (existProduct is null)
                return NotFound();

            BasketProduct basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(m => m.ProductId == productId && m.Basket.AppUserId == existUser.Id);
            Basket basket = await _context.Baskets.Include(m => m.BasketProducts).FirstOrDefaultAsync(m => m.AppUserId == existUser.Id);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                await _context.SaveChangesAsync();

                return Ok();
            }

            if (basket != null)
            {
                basket.BasketProducts.Add(new BasketProduct()
                {
                    ProductId = (int)productId,
                    Quantity = 1
                });

                await _context.SaveChangesAsync();
                return Ok();
            }

            Basket newBasket = new()
            {
                AppUserId = existUser.Id,
            };

            await _context.Baskets.AddAsync(newBasket);
            await _context.SaveChangesAsync();

            BasketProduct newBasketProduct = new()
            {
                BasketId = newBasket.Id,
                ProductId = (int)productId,
                Quantity = 1
            };

            await _context.BasketProducts.AddAsync(newBasketProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AppUser existUser = new();

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            else
            {
                existUser = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            BasketProduct basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(m => m.Id == id && m.Basket.AppUserId == existUser.Id);

            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
