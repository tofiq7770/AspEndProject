using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.Freshs
{
    public class FreshDetailVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PriceFirst { get; set; }
        [Required]
        public int PriceSecond { get; set; }

        public string? Image { get; set; }
    }
}