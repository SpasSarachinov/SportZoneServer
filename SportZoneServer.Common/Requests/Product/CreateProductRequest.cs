namespace SportZoneServer.Common.Requests.Product;

public class CreateProductRequest
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }

    public decimal RegularPrice { get; set; }

    public uint Quantity { get; set; }

    public Guid CategoryId { get; set; }
}
