using SportZoneServer.Core.Enums;

namespace SportZoneServer.Data.Entities
{
    public class Order : GenericEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; init; } = OrderStatus.Pending;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
