using AspEndProject.Models;

namespace AspEndProject.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllAsyncAscending();
        Task<Product> GetByIdAsync(int id);
        Task CreateAsync(Product product);
    }
}
