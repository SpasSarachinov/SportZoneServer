using Microsoft.AspNetCore.Identity;
using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Users.Any())
            {
                return;
            }

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            List<User> users = [];

            User admin = new User
            {
                Email = "admin@sportzone.com",
                PasswordHash = "temporaryPasswordHash",
                Names = "Admin",
                Phone = "0872123199",
                Role = "Admin"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            users.Add(admin);

            for (int i = 1; i <= 9; i++)
            {
                User customer = new User
                {
                    Email = $"customer{i}@sportzone.com",
                    PasswordHash = "temporaryPasswordHash",
                    Names = "Customer",
                    Phone = $"087212312{i}",
                    Role = "Customer"
                };
                customer.PasswordHash = hasher.HashPassword(customer, $"Customer{i}123!");
                users.Add(customer);
            }

            await db.Users.AddRangeAsync(users);
            await db.SaveChangesAsync();
        }
    }
}
