using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Products.Any())
            {
                return;
            }

            List<Category> categories = db.Categories.ToList();

            List<Product> products =
            [
                new()
                {
                    Title = "Гира 10kg",
                    Description = "Удобна хексагонална гира",
                    ImageUrl = "https://example.com/dumbbell.jpg",
                    RegularPrice = 25.99m,
                    Quantity = 10,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id
                },
                new()
                {
                    Title = "Лост за набирания",
                    Description = "За монтаж на стена",
                    ImageUrl = "https://example.com/pullup.jpg",
                    RegularPrice = 39.99m,
                    Quantity = 5,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id
                },
                new()
                {
                    Title = "Тенис Ракета Babolat",
                    Description = "Подходяща за начинаещи и напреднали",
                    ImageUrl = "https://example.com/racket.jpg",
                    RegularPrice = 79.99m,
                    Quantity = 15,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id
                },
                new()
                {
                    Title = "Футболна Топка Adidas",
                    Description = "Официална топка за мачове",
                    ImageUrl = "https://example.com/ball.jpg",
                    RegularPrice = 29.99m,
                    Quantity = 20,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id
                },
                new()
                {
                    Title = "Баскетболна Топка Spalding",
                    Description = "NBA реплика",
                    ImageUrl = "https://example.com/basketball.jpg",
                    RegularPrice = 34.99m,
                    Quantity = 12,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id
                },
                new()
                {
                    Title = "Шосеен велосипед",
                    Description = "Лек и бърз колоездачен велосипед",
                    ImageUrl = "https://example.com/bike.jpg",
                    RegularPrice = 599.99m,
                    Quantity = 3,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id
                },
                new()
                {
                    Title = "Боксови ръкавици Everlast",
                    Description = "Професионални кожени ръкавици",
                    ImageUrl = "https://example.com/gloves.jpg",
                    RegularPrice = 49.99m,
                    Quantity = 10,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id
                },
                new()
                {
                    Title = "Кънки за лед",
                    Description = "Фигурни кънки за хоби и спорт",
                    ImageUrl = "https://example.com/skates.jpg",
                    RegularPrice = 89.99m,
                    Quantity = 8,
                    CategoryId = categories.First(c => c.Name == "Хокей").Id
                },
                new()
                {
                    Title = "Катерачен въжен колан",
                    Description = "Сертифициран колан за спортно катерене",
                    ImageUrl = "https://example.com/harness.jpg",
                    RegularPrice = 69.99m,
                    Quantity = 6,
                    CategoryId = categories.First(c => c.Name == "Катерене").Id
                },
                new()
                {
                    Title = "Ски щеки",
                    Description = "Алуминиеви, олекотени",
                    ImageUrl = "https://example.com/ski-poles.jpg",
                    RegularPrice = 24.99m,
                    Quantity = 14,
                    CategoryId = categories.First(c => c.Name == "Ски").Id
                }
            ];

            foreach (Product p in products)
            {
                p.Id = Guid.NewGuid();
            }

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
