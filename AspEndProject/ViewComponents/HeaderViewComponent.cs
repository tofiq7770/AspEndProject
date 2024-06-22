using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ISettingService settingService, UserManager<AppUser> userManager, AppDbContext context)
        {
            _settingService = settingService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser existUser = new();

            if (User.Identity.IsAuthenticated)
            {
                existUser = await _userManager.FindByNameAsync(User.Identity?.Name);
            }

            int quantity = await _context.BasketProducts.Where(m => m.Basket.AppUserId == existUser.Id)
                 .SumAsync(m => m.Quantity);

            HeaderVM model = new()
            {
                Settings = await _settingService.GetAll(),
                BasketCount = quantity
            };

            return await Task.FromResult(View(model));
        }
    }
}
