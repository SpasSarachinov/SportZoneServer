using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class ReviewSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Reviews.Any())
            {
                return;
            }

            List<User> users = db.Users.Take(5).ToList();
            List<Product> products = db.Products.Take(5).ToList();

            foreach (User user in users)
            {
                foreach (Product product in products)
                {
                    db.Reviews.Add(new Review
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        ProductId = product.Id,
                        Content = $"Много добър продукт {product.Name}",
                        Rating = 4 + users.IndexOf(user) % 2
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }
}