namespace AspEndProject.Models
{
    public class Fresh : BaseEntity
    {

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PriceFirst { get; set; }
        public int PriceSecond { get; set; }
    }
}
