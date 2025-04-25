using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces;

public interface IReviewRepository : IRepository<Review>
{
    Task<IEnumerable<Review>> GetReviews(Guid productId);
}
