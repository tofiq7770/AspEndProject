using Microsoft.AspNetCore.Identity;

namespace AspEndProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
