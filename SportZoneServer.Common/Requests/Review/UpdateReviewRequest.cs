using System.ComponentModel.DataAnnotations;

namespace SportZoneServer.Common.Requests.Review;

public class UpdateReviewRequest
{
    [Required]
    public required Guid Id { get; set; }
    
    [Required]
    public required Guid ProductId { get; set; }
    
    [Required]
    public required string Content { get; set; }
    
    [Required]
    public required byte Rating { get; set; }
}
