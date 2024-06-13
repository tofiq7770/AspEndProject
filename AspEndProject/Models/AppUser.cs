using Microsoft.AspNetCore.Identity;

namespace AspEndProject.Models
{
    public class AppUser : IdentityUser
    {
        //public string? Image { get; set; } = $"https://static.vecteezy.com/system/resources/previews/019/879/186/non_2x/user-icon-on-transparent-background-free-png.png";
        public string FullName { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
