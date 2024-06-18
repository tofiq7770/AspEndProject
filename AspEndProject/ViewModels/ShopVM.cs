using AspEndProject.Models;
using AspEndProject.ViewModels.Category;

namespace AspEndProject.ViewModels
{
    public class ShopVM
    {

        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<CategoryVM> Categories { get; set; }

    }
}
