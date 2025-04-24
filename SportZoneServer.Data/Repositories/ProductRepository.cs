using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : Repository<Product>(context), IProductRepository
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<Product>> GetBestSellersAsync(int numOfBestSellers)
    {
        IQueryable<Product> query = _context.Set<Product>().AsQueryable();
        PropertyInfo? isDeletedProperty = typeof(Product).GetProperty("IsDeleted");

        if (isDeletedProperty != null && isDeletedProperty.PropertyType == typeof(bool))
        {
            query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
        }

        return await query
            .Include(x => x.Category)
            .OrderBy(x => Guid.NewGuid())
            .Take(numOfBestSellers)
            .ToListAsync();
    }
}
