using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class OrderItemRepository(ApplicationDbContext context) : Repository<OrderItem>(context), IOrderItemRepository
{
    public async Task<bool> AddRange(ICollection<OrderItem> orderItems)
    {
        if (orderItems is null)
        {
            return false;
        }
        if (orderItems.Count == 0)
        {
            return false;
        }
        
        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
        
        return true;
    }
}
