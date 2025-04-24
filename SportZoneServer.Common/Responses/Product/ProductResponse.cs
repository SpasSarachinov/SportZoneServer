namespace SportZoneServer.Common.Responses.Product;

public class ProductResponse
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }
    
    public required string ImageUrl { get; set; }

    public decimal RegularPrice { get; set; }

    public uint Quantity { get; set; }

    public Guid CategoryId { get; set; }
    
    public required string CategoryName { get; set; }
}
