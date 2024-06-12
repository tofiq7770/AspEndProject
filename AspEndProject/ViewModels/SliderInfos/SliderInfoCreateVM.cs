using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.SliderInfos
{
    public class SliderInfoCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
    }
}
