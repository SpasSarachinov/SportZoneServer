using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Category;

public class UpdateCategoryRequest 
{
    [Required]
    public required Guid Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string ImageURI { get; set; }
}
