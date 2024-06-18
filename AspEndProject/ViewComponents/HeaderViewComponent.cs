using AspEndProject.Services.Interface;
using AspEndProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;


        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            Dictionary<string, string> settings = await _settingService.GetAll();

            HeaderVM model = new()
            {
                Settings = settings
            };

            return await Task.FromResult(View(model));
        }
    }
}
