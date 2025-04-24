using SportZoneServer.Common.Responses.Product;

namespace SportZoneServer.Common.Responses.Wishlist;

public class WishlistResponse
{
    public ICollection<ProductsResponse> Products { get; set; } = new List<ProductsResponse>();
}
