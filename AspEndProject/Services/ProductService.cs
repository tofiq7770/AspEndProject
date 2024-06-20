using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(m => m.Category)
                .Include(m => m.ProductImages)
                .Include(m => m.Reviews)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }
        public async Task<List<Product>> GetAllAsyncAscending()
        {
            return await _context.Products
                .Include(m => m.Category)
                .Include(m => m.ProductImages)
                .Include(m => m.Reviews)
                .OrderBy(m => m.Id)
                .ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(m => m.Category)
                                         .Include(m => m.ProductImages)
                                         .Include(m => m.Reviews)
                                         .ThenInclude(m => m.AppUser)
                                         .Where(m => !m.SoftDelete)
                                         .FirstOrDefaultAsync(m => m.Id == id);
        }


    }
}
