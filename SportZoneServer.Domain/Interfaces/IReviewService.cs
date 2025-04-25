using SportZoneServer.Common.Requests.Review;
using SportZoneServer.Common.Responses.Review;
using SportZoneServer.Core.Pages;

namespace SportZoneServer.Domain.Interfaces;

public interface IReviewService
{
    Task<ReviewResponse?> UpdateAsync(UpdateReviewRequest request);
    Task<ReviewResponse?> CreateAsync(CreateReviewRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<Paginated<ReviewResponse>> SearchReviewsAsync(SearchReviewsRequest request);
}
