using Ecommerce.Models;
using Ecommerce.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<User> UserManager { get; }
        

        public AccountController(UserManager<User> userManager
            )
        {
            this.UserManager = userManager;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel User)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel User)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = User.Email;
                user.UserName = User.UserName;
                user.PasswordHash = User.Password;

                IdentityResult result = await UserManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    // Create Cookie
                    return View("login");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
            }
			return View();
		}
    }
}
