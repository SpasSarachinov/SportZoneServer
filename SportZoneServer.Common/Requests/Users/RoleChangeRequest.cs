using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Users;

public class RoleChangeRequest
{
    [Required]
    public required Guid UserId { get; set; }
}
