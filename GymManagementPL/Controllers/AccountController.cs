using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModel;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountServices accountServices, SignInManager<ApplicationUser> signInManager)
        {
            _accountServices = accountServices;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var User = _accountServices.ValidateUser(model);
            if (User is null)
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
            var Result = _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;
            if (Result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Account Is Not Allowed");
                return View(model);
            }
            if (Result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Account Is Locked Out");
            if (Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
