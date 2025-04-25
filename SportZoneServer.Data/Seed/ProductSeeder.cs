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
                    PrimaryImageUrl = "https://example.com/dumbbell.jpg",
                    RegularPrice = 25.99m,
                    DiscountedPrice = 25.99m,
                    Quantity = 10,
                    Rating = 1,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id
                },
                new()
                {
                    Title = "Фитнес постелка",
                    Description = "Неплъзгаща се постелка за тренировки у дома",
                    PrimaryImageUrl = "https://example.com/mat.jpg",
                    RegularPrice = 19.99m,
                    DiscountedPrice = 19.99m,
                    Quantity = 25,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Фитнес").Id
                },

                new()
                {
                    Title = "Тенис Ракета Babolat",
                    Description = "Подходяща за начинаещи и напреднали",
                    PrimaryImageUrl = "https://example.com/racket.jpg",
                    RegularPrice = 79.99m,
                    DiscountedPrice = 79.99m,
                    Quantity = 15,
                    Rating = 3,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id
                },
                new()
                {
                    Title = "Туба с тенис топки (3 броя)",
                    Description = "Официални топки за турнир",
                    PrimaryImageUrl = "https://example.com/tennis-balls.jpg",
                    RegularPrice = 9.99m,
                    DiscountedPrice = 9.99m,
                    Quantity = 40,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Тенис").Id
                },

                new()
                {
                    Title = "Футболна Топка Adidas",
                    Description = "Официална топка за мачове",
                    PrimaryImageUrl = "https://example.com/ball.jpg",
                    RegularPrice = 29.99m,
                    DiscountedPrice = 29.99m,
                    Quantity = 20,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id
                },
                new()
                {
                    Title = "Футболни обувки Nike",
                    Description = "Подходящи за изкуствени терени",
                    PrimaryImageUrl = "https://example.com/boots.jpg",
                    RegularPrice = 89.99m,
                    DiscountedPrice = 89.99m,
                    Quantity = 12,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Футбол").Id
                },

                new()
                {
                    Title = "Баскетболна Топка Spalding",
                    Description = "NBA реплика",
                    PrimaryImageUrl = "https://example.com/basketball.jpg",
                    RegularPrice = 34.99m,
                    DiscountedPrice = 34.99m,
                    Quantity = 12,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id
                },
                new()
                {
                    Title = "Мрежа за баскетболен кош",
                    Description = "Издръжлива и устойчива на атмосферни условия",
                    PrimaryImageUrl = "https://example.com/net.jpg",
                    RegularPrice = 12.99m,
                    DiscountedPrice = 12.99m,
                    Quantity = 30,
                    Rating = 3,
                    CategoryId = categories.First(c => c.Name == "Баскетбол").Id
                },

                new()
                {
                    Title = "Шосеен велосипед",
                    Description = "Лек и бърз колоездачен велосипед",
                    PrimaryImageUrl = "https://example.com/bike.jpg",
                    RegularPrice = 599.99m,
                    DiscountedPrice = 599.99m,
                    Quantity = 3,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id
                },
                new()
                {
                    Title = "Каска за колоездене",
                    Description = "Вентилирана каска с регулируем ремък",
                    PrimaryImageUrl = "https://example.com/helmet.jpg",
                    RegularPrice = 39.99m,
                    DiscountedPrice = 39.99m,
                    Quantity = 10,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Колоездене").Id
                },

                new()
                {
                    Title = "Боксови ръкавици Everlast",
                    Description = "Професионални кожени ръкавици",
                    PrimaryImageUrl = "https://example.com/gloves.jpg",
                    RegularPrice = 49.99m,
                    DiscountedPrice = 49.99m,
                    Quantity = 10,
                    Rating = 5,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id
                },
                new()
                {
                    Title = "Боксов чувал 25kg",
                    Description = "Здрав чувал за домашни тренировки",
                    PrimaryImageUrl = "https://example.com/punching-bag.jpg",
                    RegularPrice = 89.99m,
                    DiscountedPrice = 89.99m,
                    Quantity = 7,
                    Rating = 4,
                    CategoryId = categories.First(c => c.Name == "Бокс").Id
                }
            ];

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
