using System.ComponentModel.DataAnnotations;

namespace AspEndProject.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }

}
