using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_32.Data {
    public class ContextSeed {


        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {

            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Traveler.ToString()));

        }

        public static async Task SuperSeedRoleAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {

            var superUser = new User {
                UserName = "admin",
                Email = "admin@adminsupport.com",
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };


            if (userManager.Users.All(u => u.Id != superUser.Id)) {

                var user = await userManager.FindByEmailAsync(superUser.Email);
                if (user == null) {
                    await userManager.CreateAsync(superUser, "P@ssword12$");


                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Traveler.ToString());

                }



            }
        }
    }
}
