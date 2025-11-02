using EcommerceApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Infrastructure.Identity
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services, ILogger logger)
        {

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Cliente" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    logger.LogInformation($"Papel (Role) '{roleName}' criado.");
                }
            }

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            string adminEmail = "admin@ecommerce.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    logger.LogInformation($"Usuário Admin '{adminEmail}' criado e adicionado ao papel 'Admin'.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogError(error.Description);
                    }
                }
            }
        }
    }
}