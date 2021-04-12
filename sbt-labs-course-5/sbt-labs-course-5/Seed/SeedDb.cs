using Microsoft.AspNetCore.Identity;
using sbt_labs_course_5.Models;
using sbt_labs_course_5.Models.Constants;
using sbt_labs_course_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Seed
{
    public static class SeedDb
    {
        public static async Task SeedAdminUserAsync(ApplicationDbContext context, UserManager<User> userManager)
        {
            string adminUserPassword = "P@ssword123!";
            User adminUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"

            };

            var user = await userManager.FindByNameAsync(adminUser.UserName);

            if (user == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminUserPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await context.SaveChangesAsync();
                }

            }

        }

        public static async Task CreateRoles(ApplicationDbContext context, RoleManager<Role> roleManager)
        {
            string[] roleNames = {
                                UserRoleType.Admin,
                                UserRoleType.Student,
                                UserRoleType.User
                                };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role
                    {
                        Name = roleName
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
