using System.ComponentModel.DataAnnotations;

namespace AspEndProject.Areas.ViewModels.Products
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public decimal MinWeight { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Quality { get; set; }
        [Required]
        public string Check { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
