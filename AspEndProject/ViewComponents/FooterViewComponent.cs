using AspEndProject.Services.Interface;
using AspEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> settings = await _settingService.GetAll();

            FooterVM model = new()
            {
                Settings = settings
            };

            return await Task.FromResult(View(model));
        }
    }
}
