using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Category;

public class UpdateCategoryRequest : CreateCategoryRequest
{
    [Required]
    public required Guid Id { get; set; }
}
