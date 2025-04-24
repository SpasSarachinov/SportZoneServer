using SportZoneServer.Common.Responses.Wishlist;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class WishlistService : IWishlistService
{
    public async Task<WishlistResponse> GetByJWT()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddProductToWishlistAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveProductFromWishlistAsync(Guid productId)
    {
        throw new NotImplementedException();
    }
}
