using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Categories.Any())
            {
                return;
            }

            Image[] images = [
            new() { Uri = "https://api.interactive-img.com/interactiveimage/67171ba1c4f8e.jpg"},
            new() { Uri = "https://images.unsplash.com/photo-1595435934249-5df7ed86e1c0?q=80&w=800"},
            new() { Uri = "https://images.unsplash.com/photo-1579952363873-27f3bade9f55?q=80&w=800"},
            new() { Uri = "https://images.unsplash.com/photo-1546519638-68e109498ffc?q=80&w=800"},
            new() { Uri = "https://image-cdn.essentiallysports.com/wp-content/uploads/2021-07-15T142945Z_720664159_UP1EH7F149JI4_RTRMADP_3_CYCLING-FRANCE-800x546.jpg"},
            new() { Uri = "https://pride.shop/images/thumbs/0013562_rukavice.jpeg"},
            ];
            
            db.Images.AddRange(images);
            await db.SaveChangesAsync();
            
            Category[] categories =
            [
                new() { Id = Guid.NewGuid(), Name = "Фитнес", ImageId =  images[0].Id },
                new() { Id = Guid.NewGuid(), Name = "Тенис", ImageId = images[1].Id },
                new() { Id = Guid.NewGuid(), Name = "Футбол", ImageId = images[2].Id },
                new() { Id = Guid.NewGuid(), Name = "Баскетбол", ImageId = images[3].Id },
                new() { Id = Guid.NewGuid(), Name = "Колоездене", ImageId = images[4].Id },
                new() { Id = Guid.NewGuid(), Name = "Бокс", ImageId = images[5].Id },
            ];

            db.Categories.AddRange(categories);
            await db.SaveChangesAsync();
        }
    }
}
