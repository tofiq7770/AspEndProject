using AspEndProject.Models;

namespace AspEndProject.ViewModels.Products
{
    public class ProductCommentVM
    {

        public Product Product { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
    }
}
