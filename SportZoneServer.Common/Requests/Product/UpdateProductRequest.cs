namespace SportZoneServer.Common.Requests.Product;

public class UpdateProductRequest
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string ImageUrl { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid CategoryId { get; set; }
}
