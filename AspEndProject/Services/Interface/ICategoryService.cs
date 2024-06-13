using AspEndProject.ViewModels.Category;

namespace AspEndProject.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetALlCategories();
    }
}
