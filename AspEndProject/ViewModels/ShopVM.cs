using AspEndProject.Models;
using AspEndProject.ViewModels.Category;
using AspEndProject.ViewModels.Products;

namespace AspEndProject.ViewModels
{
    public class ShopVM
    {
        public ProductCommentVM ProductCommentVM { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public Review Review { get; set; }
        public List<CategoryVM> Categories { get; set; }


    }
}
