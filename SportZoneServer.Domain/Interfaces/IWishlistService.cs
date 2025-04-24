using SportZoneServer.Common.Responses.Wishlist;

namespace SportZoneServer.Domain.Interfaces;

public interface IWishlistService
{
    Task<WishlistResponse> GetByJWT();
    Task<bool> AddProductToWishlistAsync(Guid productId);
    Task<bool> RemoveProductFromWishlistAsync(Guid productId);
}
