namespace AspEndProject.Models
{
    public class Basket : BaseEntity
    {

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
