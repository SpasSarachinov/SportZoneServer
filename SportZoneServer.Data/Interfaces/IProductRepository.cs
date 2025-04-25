using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetBestSellersAsync(int numOfBestSellers);

    Task UpdateRatingAsync(Guid productId, double rating);
}
