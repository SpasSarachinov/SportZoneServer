using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Users;

public class CreateUserRequest
{
    [Required]
    public required string Email { get; set; } 
    
    [Required]
    public required string Password { get; set; } 
    
    [Required]
    public required string Names { get; set; } 
    
    [Required]
    public required string Phone { get; set; } 
}
