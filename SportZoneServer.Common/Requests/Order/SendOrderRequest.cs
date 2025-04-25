using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Order;

public class SendOrderRequest
{
    [Required]
    public required string Names { get; set; }
    
    [Required]
    public required string PostalCode { get; set; }
    
    [Required]
    public required string Country { get; set; }
    
    [Required]
    public required string City { get; set; }
    
    [Required]
    public required string Address { get; set; }
    
    [Required]
    public required string Phone { get; set; }
}
