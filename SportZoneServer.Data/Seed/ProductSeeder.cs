using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Products.Any()) return;

            var categories = db.Categories.ToList();

            var products = new List<Product>
            {
                new() { Name = "Гира 10kg", Description = "Удобна хексагонална гира", ImageUrl = "https://example.com/dumbbell.jpg", Price = 25.99m, Quantity = 10, CategoryId = categories.First(c => c.Name == "Фитнес").Id },
                new() { Name = "Лост за набирания", Description = "За монтаж на стена", ImageUrl = "https://example.com/pullup.jpg", Price = 39.99m, Quantity = 5, CategoryId = categories.First(c => c.Name == "Фитнес").Id },
                new() { Name = "Тенис Ракета Babolat", Description = "Подходяща за начинаещи и напреднали", ImageUrl = "https://example.com/racket.jpg", Price = 79.99m, Quantity = 15, CategoryId = categories.First(c => c.Name == "Тенис").Id },
                new() { Name = "Футболна Топка Adidas", Description = "Официална топка за мачове", ImageUrl = "https://example.com/ball.jpg", Price = 29.99m, Quantity = 20, CategoryId = categories.First(c => c.Name == "Футбол").Id },
                new() { Name = "Баскетболна Топка Spalding", Description = "NBA реплика", ImageUrl = "https://example.com/basketball.jpg", Price = 34.99m, Quantity = 12, CategoryId = categories.First(c => c.Name == "Баскетбол").Id },
                new() { Name = "Шосеен велосипед", Description = "Лек и бърз колоездачен велосипед", ImageUrl = "https://example.com/bike.jpg", Price = 599.99m, Quantity = 3, CategoryId = categories.First(c => c.Name == "Колоездене").Id },
                new() { Name = "Боксови ръкавици Everlast", Description = "Професионални кожени ръкавици", ImageUrl = "https://example.com/gloves.jpg", Price = 49.99m, Quantity = 10, CategoryId = categories.First(c => c.Name == "Бокс").Id },
                new() { Name = "Кънки за лед", Description = "Фигурни кънки за хоби и спорт", ImageUrl = "https://example.com/skates.jpg", Price = 89.99m, Quantity = 8, CategoryId = categories.First(c => c.Name == "Хокей").Id },
                new() { Name = "Катерачен въжен колан", Description = "Сертифициран колан за спортно катерене", ImageUrl = "https://example.com/harness.jpg", Price = 69.99m, Quantity = 6, CategoryId = categories.First(c => c.Name == "Катерене").Id },
                new() { Name = "Ски щеки", Description = "Алуминиеви, олекотени", ImageUrl = "https://example.com/ski-poles.jpg", Price = 24.99m, Quantity = 14, CategoryId = categories.First(c => c.Name == "Ски").Id }
            };

            foreach (var p in products)
            {
                p.Id = Guid.NewGuid();
            }

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}