using Microsoft.EntityFrameworkCore;
using SportZoneServer.Core.Enums;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class OrderRepository(ApplicationDbContext context) : Repository<Order>(context), IOrderRepository
{
    public async Task<Order?> GetByUserIdAsync(Guid userId)
    {
        return await context.Orders
            .Include(x => x.User)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Status == OrderStatus.Created && !x.IsDeleted);
    }

    public async Task<Order> AddAsync(Guid userId)
    {
        Order newOrder = new()
        {
            UserId = userId,
        };
        
        await context.Orders.AddAsync(newOrder);
        await context.SaveChangesAsync();
        
        return newOrder;
    }
}
