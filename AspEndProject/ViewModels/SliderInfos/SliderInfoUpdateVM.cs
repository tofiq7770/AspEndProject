using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.SliderInfos
{
    public class SliderInfoUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
    }
}
