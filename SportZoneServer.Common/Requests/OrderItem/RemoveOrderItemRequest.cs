using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.OrderItem;

public class RemoveOrderItemRequest
{
    public required Guid ProductId { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public required int Quantity { get; set; }
}
