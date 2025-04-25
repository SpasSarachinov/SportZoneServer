using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class ReviewRepository(ApplicationDbContext context) : Repository<Review>(context), IReviewRepository
{
    public async Task<IEnumerable<Review>> GetReviews(Guid productId)
    {
        return context.Reviews.Where(r => r.ProductId == productId && !r.IsDeleted);
    }
}
