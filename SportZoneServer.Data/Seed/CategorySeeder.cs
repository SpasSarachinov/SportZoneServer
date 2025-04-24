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

            Category[] categories =
            [
                new() { Id = Guid.NewGuid(), Name = "Фитнес" },
                new() { Id = Guid.NewGuid(), Name = "Тенис" },
                new() { Id = Guid.NewGuid(), Name = "Футбол" },
                new() { Id = Guid.NewGuid(), Name = "Баскетбол" },
                new() { Id = Guid.NewGuid(), Name = "Колоездене" },
                new() { Id = Guid.NewGuid(), Name = "Бокс" },
                new() { Id = Guid.NewGuid(), Name = "Хокей" },
                new() { Id = Guid.NewGuid(), Name = "Катерене" },
                new() { Id = Guid.NewGuid(), Name = "Гмуркане" },
                new() { Id = Guid.NewGuid(), Name = "Ски" }
            ];

            db.Categories.AddRange(categories);
            await db.SaveChangesAsync();
        }
    }
}