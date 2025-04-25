using SportZoneServer.Common.Responses.OrderItem;

namespace SportZoneServer.Common.Responses.Order;

public class OrderResponse
{
    public required Guid Id { get; set; }
    public required decimal PriceBeforeDiscount { get; set; }
    public required decimal PriceAfterDiscount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public ICollection<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
}
