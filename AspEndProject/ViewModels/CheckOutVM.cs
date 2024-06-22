using AspEndProject.Models;
using AspEndProject.ViewModels.Basket;
using AspEndProject.ViewModels.Category;

namespace AspEndProject.ViewModels
{
    public class CheckOutVM
    {

        public List<Product> Products { get; set; }
        public List<BasketListVM> BasketLists { get; set; }
        public List<CategoryVM> Categories { get; set; }
    }
}
