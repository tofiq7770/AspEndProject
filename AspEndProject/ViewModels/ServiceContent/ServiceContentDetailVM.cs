using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.ServiceContent
{
    public class ServiceContentDetailVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
