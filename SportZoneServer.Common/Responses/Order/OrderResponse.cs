using SportZoneServer.Common.Responses.OrderItem;
using SportZoneServer.Core.Enums;

namespace SportZoneServer.Common.Responses.Order;

public class OrderResponse
{
    public required Guid Id { get; set; }
    public required decimal OrderTotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public ICollection<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
}
