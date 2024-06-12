namespace AspEndProject.ViewModels.Sliders
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
