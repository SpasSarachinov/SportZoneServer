using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Responses.User;

public class UserResponse
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public required string Email { get; set; }
}
