using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Image;

public class CreateImageRequest
{
    [Required]
    public required string Uri { get; set; }
}
