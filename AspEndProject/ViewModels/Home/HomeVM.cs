using AspEndProject.Models;
using AspEndProject.ViewModels.Category;

namespace AspEndProject.ViewModels.Home
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
        public List<CategoryVM> Categories { get; set; }
        //public List<Slider> Sliders { get; set; }
        //public SliderInfo SliderInfo { get; set; }
        public List<FactContent> FactContents { get; set; }
        public List<Feature> Features { get; set; }
        public List<ServiceContent> ServiceContents { get; set; }
        public Fresh Fresh { get; set; }
    }
}
