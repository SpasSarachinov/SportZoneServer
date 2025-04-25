using SportZoneServer.Common.Requests.Image;

namespace SportZoneServer.Common.Requests.Product;

public class UpdateProductRequest
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string PrimaryImageUrl { get; set; }

    public decimal RegularPrice { get; set; }

    public uint Quantity { get; set; }

    public Guid CategoryId { get; set; }
    
    public ICollection<UpdateImageRequest> SecondaryImages { get; set; } 

}
