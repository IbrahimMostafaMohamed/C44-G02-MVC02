using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> RoleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = RoleManager.Roles.Any();
                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new(){Name = "SuperAdmin"} ,
                        new(){Name = "Admin"} ,
                    };
                    foreach (var role in Roles)
                    {
                        if(!RoleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            RoleManager.CreateAsync(role).Wait();
                        }

                    }
                }

                if(!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Ibrahim",
                        LastName = "Mostafa",
                        UserName = "IbrahimMostafa",
                        Email = "Ibrahim@gmail.com",
                        PhoneNumber = "01234567899"
                    };
                    userManager.CreateAsync(MainAdmin , "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Rawan",
                        LastName = "Tarek",
                        UserName = "RawanTarek",
                        Email = "Rawan@gmail.com",
                        PhoneNumber = "01234567890"
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }

                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Seed Failed {Ex}");
                return false;
            }
        }
    }
}
