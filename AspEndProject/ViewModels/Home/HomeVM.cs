using AspEndProject.Models;
using AspEndProject.ViewModels.Category;

namespace AspEndProject.ViewModels.Home
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
