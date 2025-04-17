using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = new[] { "Admin", "User" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            string adminEmail = "admin@sportzone.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            for (int i = 1; i <= 9; i++)
            {
                string email = $"user{i}@mail.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    User user = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, "User123!");
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}