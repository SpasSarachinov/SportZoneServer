using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class WishlistRepository(ApplicationDbContext context) : Repository<WishlistItem>(context), IWishlistRepository
{
    
}
