using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Users;

public class RoleChangeRequest
{
    [Required]
    public required Guid Id { get; set; }
}
