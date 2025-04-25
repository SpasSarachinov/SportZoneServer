using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task<bool> AddRange(ICollection<OrderItem> orderItems);
}
