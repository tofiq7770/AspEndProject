using AspEndProject.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspEndProject.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllCategories();
        Task<List<CategoryVM>> GetAllCategoriesAsc();
        Task<SelectList> GetAllBySelectedAsync();
    }
}
