using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.FactContents
{
    public class FactContentCreateVM
    {
        [Required]
        public string Icon { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Max Length is 30")]
        public string Title { get; set; }
        [Required]
        public int NumberInfo { get; set; }
    }
}
