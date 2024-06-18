using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.ServiceContents
{
    public class ServiceContentCreateVM
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
