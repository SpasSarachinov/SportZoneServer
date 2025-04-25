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

            
        }
    }
}
