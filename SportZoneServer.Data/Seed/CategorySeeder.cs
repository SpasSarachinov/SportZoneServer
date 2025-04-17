using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Seed
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Categories.Any()) return;

            var categories = new[]
            {
                new Category { Id = Guid.NewGuid(), Name = "Фитнес" },
                new Category { Id = Guid.NewGuid(), Name = "Тенис" },
                new Category { Id = Guid.NewGuid(), Name = "Футбол" },
                new Category { Id = Guid.NewGuid(), Name = "Баскетбол" },
                new Category { Id = Guid.NewGuid(), Name = "Колоездене" },
                new Category { Id = Guid.NewGuid(), Name = "Бокс" },
                new Category { Id = Guid.NewGuid(), Name = "Хокей" },
                new Category { Id = Guid.NewGuid(), Name = "Катерене" },
                new Category { Id = Guid.NewGuid(), Name = "Гмуркане" },
                new Category { Id = Guid.NewGuid(), Name = "Ски" }
            };

            db.Categories.AddRange(categories);
            await db.SaveChangesAsync();
        }
    }
}