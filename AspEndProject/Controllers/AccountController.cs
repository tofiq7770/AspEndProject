using AspEndProject.Helpers.Enums;
using AspEndProject.Models;
using AspEndProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspEndProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid) { return View(); }

            AppUser user = new()
            {
                UserName = request.Username,
                Email = request.Email,
                FullName = request.Fullname
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);
            }

            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "Email, Username or Password is incorrect");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemember, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Server is eneabled at the moment,please try again later");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email, Username or Password is incorrect");
                return View();
            }


            await _signInManager.SignInAsync(user, login.IsRemember);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
