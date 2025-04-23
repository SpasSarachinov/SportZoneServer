using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Category;

public class CreateCategoryRequest
{
    [Required]
    public required string Name { get; set; }
}
