using SportZoneServer.Common.Responses.OrderItem;

namespace SportZoneServer.Common.Responses.Order;

public class OrderResponse
{
    public required Guid Id { get; set; }
    public required decimal OrderTotalPrice { get; set; }
    public ICollection<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
}
