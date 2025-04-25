using SportZoneServer.Core.Enums;
using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class OrderSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Orders.Any())
            {
                return;
            }

            List<User> users = db.Users.Take(5).ToList();
            List<Product> products = db.Products.Take(3).ToList();

            foreach (User user in users)
            {
                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    Status = OrderStatus.Created,
                    Items = new List<OrderItem>()
                };

                foreach (Product product in products)
                {
                    order.Items.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Quantity = 1 + users.IndexOf(user),
                        SinglePrice = product.RegularPrice
                    });
                }

                db.Orders.Add(order);
            }

            await db.SaveChangesAsync();
        }
    }
}
