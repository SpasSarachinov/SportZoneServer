using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
 Task<Order?> GetByUserIdAsync(Guid userId);   

 Task<Order?> GetByUserIdWithoutStatusRestrictionAsync(Guid userId);   

 Task<Order> AddAsync(Guid userId);   

}
