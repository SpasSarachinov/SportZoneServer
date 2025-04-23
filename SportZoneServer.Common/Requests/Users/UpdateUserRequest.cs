using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Users;

public class UpdateUserRequest : CreateUserRequest
{
    [Required]
    public Guid Id { get; set; } 
}
