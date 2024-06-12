using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Name { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
