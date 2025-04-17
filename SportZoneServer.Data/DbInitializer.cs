using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportZoneServer.Data.Seed;

namespace SportZoneServer.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope scope = scopeFactory.CreateScope();

            ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await CategorySeeder.SeedAsync(db);
            await ProductSeeder.SeedAsync(db);
            await UserSeeder.SeedAsync(scope.ServiceProvider);
            await ReviewSeeder.SeedAsync(db);
            await OrderSeeder.SeedAsync(db);
            await WishlistSeeder.SeedAsync(db);
        }
    }
}