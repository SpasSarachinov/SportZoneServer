namespace SportZoneServer.Common.Requests.OrderItem;

public class AddOrderItemRequest
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}
