using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
