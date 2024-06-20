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

        [Range(1, 5, ErrorMessage = "Value must be at least 1 and Max 5")]
        public int? Rating { get; set; } = 0;

        [Required]
        public string Check { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
