using SportZoneServer.Common.Responses.Product;

namespace SportZoneServer.Common.Responses.Wishlist;

public class WishlistResponse
{
    public ICollection<ProductResponse> Products { get; set; } = new List<ProductResponse>();
}
