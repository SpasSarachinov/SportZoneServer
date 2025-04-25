using System.ComponentModel.DataAnnotations;
using SportZoneServer.Core.Enums;

namespace SportZoneServer.Common.Requests.Order;

public class ChangeOrderStatusRequest
{
    [Required]
    public required Guid OrderId { get; set; }
    
    [Required]
    public required OrderStatus OrderStatus { get; set; }
}
