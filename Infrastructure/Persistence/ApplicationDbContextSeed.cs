using Domain.Enums;
using Infrastructure.Identity;
using InventoryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole { Name = "Admin", Description = "details goes here" };


            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { Id = Guid.Parse("950B25C2-5BED-7663-2392-08DB223FA0E8"), UserName = "InventoryAdmin", Email = "admin@inventory.com.np", IsActive = true, Address = "Dillibazar, Kathmandu", EmailConfirmed = true, PhoneNumber = "011213141", UserType=UserType.Admin };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Inventory@123");

                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });

                var enumValues = Enum.GetNames(typeof(Permission));

                foreach (var permission in enumValues)
                {
                    if (Enum.IsDefined(typeof(Domain.Enums.Permission), permission))
                    {
                        // convert string to enum, invalid cast will throw an exception
                        Permission enumValue = (Permission)Enum.Parse(typeof(Permission), permission);

                        // convert an enum to an int
                        int integerValueOfEnum = (int)enumValue;


                        object value = await roleManager.AddClaimAsync(administratorRole, new Claim(CustomClaimTypes.Permission, integerValueOfEnum.ToString()));
                    }
                }
            }
        }
    }
}
