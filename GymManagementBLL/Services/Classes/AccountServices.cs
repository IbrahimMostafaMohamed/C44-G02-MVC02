using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModel;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementBLL.Services.Classes
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
            var User = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (User == null) return null;
            var IsPasswordValid = _userManager.CheckPasswordAsync(User,loginViewModel.Password).Result; 
            return IsPasswordValid ? User : null;

        }
    }
}
