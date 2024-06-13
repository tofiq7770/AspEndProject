using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.Features
{
    public class FeatureCreateVM
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Name { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string Description { get; set; }
    }
}
