using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryVM>> GetALlCategories()
        {
            List<Category> categories = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();
            return categories.Select(m => new CategoryVM { Id = m.Id, Name = m.Name }).ToList();
        }
    }
}
