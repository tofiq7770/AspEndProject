using System.ComponentModel.DataAnnotations.Schema;

namespace AspEndProject.Models
{
    public class Product : BaseNameableEntity
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public string Origin { get; set; }
        public string Quality { get; set; }
        public string Сheck { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal MinWeight { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
