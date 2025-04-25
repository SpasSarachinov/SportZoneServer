using SportZoneServer.Common.Requests.Review;
using SportZoneServer.Common.Responses.Review;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Core.Pages;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository, IAuthService authService) : IReviewService
{
    public async Task<ReviewResponse?> UpdateAsync(UpdateReviewRequest request)
    {
        Review? existingReview = await reviewRepository.GetByIdAsync(request.Id);
        if (existingReview == null)
        {
            throw new AppException("Review not found.").SetStatusCode(404);
        }

        existingReview.Content = request.Content;
        existingReview.Rating = request.Rating;
        
        Review updatedReview = await reviewRepository.UpdateAsync(existingReview);

        await RecalculateProductRatingAsync(updatedReview!.ProductId);
        
        return new()
        {
            Id = updatedReview.Id,
            Content = updatedReview.Content,
            Rating = updatedReview.Rating,
            CreatedOn = updatedReview.CreatedOn,   
         
        };    
    }

    public async Task<ReviewResponse?> CreateAsync(CreateReviewRequest request)
    {
        Review newReview = new()
        {
            ProductId = request.ProductId,
            UserId = Guid.Parse(await authService.GetCurrentUserId()),
            Content = request.Content,
            Rating = request.Rating,
        };
        
        await reviewRepository.AddAsync(newReview);
        
        await RecalculateProductRatingAsync(newReview.ProductId);

        return new()
        {
            Id = newReview.Id,
            Content = request.Content,
            Rating = newReview.Rating,
            CreatedOn = newReview.CreatedOn,
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Review? review = await reviewRepository.GetByIdAsync(id);
        if (review == null)
        {
            throw new AppException("Product not found.").SetStatusCode(404);
        }

        if (review.UserId != Guid.Parse(await authService.GetCurrentUserId()))
        {
            throw new AppException("Can't delete a review that is not yours.").SetStatusCode(403);
        }
        
        return await reviewRepository.DeleteAsync(id);    
    }

    public async Task<Paginated<ReviewResponse>> SearchReviewsAsync(SearchReviewsRequest request)
    {
        Filter<Review> filter = new()
        {
            Includes = 
            [
                x => x.Product!
            ],
            Predicate = request.GetPredicate(),
            PageNumber = request.PageNumber ?? 1,
            PageSize = request.PageSize ?? 10,
            SortBy = request.SortBy ?? "CreatedOn",
            SortDescending = request.SortDescending ?? false,
        };

        Paginated<Review> result = await reviewRepository.SearchAsync(filter);

        List<ReviewResponse> responses = new();

        foreach (Review review in result.Items!)
        {
            ReviewResponse response = new()
            {
                Id = review.Id,
                Content = review.Content,
                Rating = review.Rating,
                CreatedOn = review.CreatedOn,
                UserId = review.UserId,
            };

            responses.Add(response);
        }

        Paginated<ReviewResponse> paginated = new()
        {
            Items = responses,
            TotalCount = result.TotalCount
        };

        return paginated;    
    }

    private async Task RecalculateProductRatingAsync(Guid productId)
    {
        IEnumerable<Review> reviews = await reviewRepository.GetReviews(productId);

        double rating = 0;

        if (reviews.Any())
        {
            rating = reviews.Sum(x => x.Rating) / reviews.Count();
            rating = Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2.0;
        }
        
        await productRepository.UpdateRatingAsync(productId, rating);
    }
}
