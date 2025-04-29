using Moq;
using SportZoneServer.Common.Requests.Review;
using SportZoneServer.Common.Responses.Review;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportZoneServer.Core.Pages;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Domain.Interfaces;
using Xunit;

namespace SportZoneServer.Tests.Unit.Services
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> reviewRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IAuthService> authServiceMock;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly ReviewService reviewService;

        public ReviewServiceTests()
        {
            reviewRepositoryMock = new Mock<IReviewRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            authServiceMock = new Mock<IAuthService>();
            userRepositoryMock = new Mock<IUserRepository>();
            reviewService = new ReviewService(
                reviewRepositoryMock.Object,
                productRepositoryMock.Object,
                authServiceMock.Object,
                userRepositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_ReviewNotFound_ShouldThrowNotFound()
        {
            UpdateReviewRequest request = new UpdateReviewRequest
            {
                Id = Guid.NewGuid(),
                Content = null,
                Rating = 0
            };
            reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Review)null);

            await Assert.ThrowsAsync<AppException>(() => reviewService.UpdateAsync(request));
        }

        [Fact]
        public async Task UpdateAsync_ValidReview_ShouldReturnUpdatedReview()
        {
            UpdateReviewRequest request = new UpdateReviewRequest
            {
                Id = Guid.NewGuid(),
                Content = "Updated Content",
                Rating = 5
            };

            Review existingReview = new Review { Id = request.Id, Content = "Old Content", Rating = 3 };
            reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(existingReview);
            reviewRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Review>())).ReturnsAsync(existingReview);
            userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User
            {
                Names = "John Doe",
                Email = null,
                Phone = null
            });

            ReviewResponse result = await reviewService.UpdateAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Updated Content", result.Content);
            Assert.Equal(5, result.Rating);
        }

        [Fact]
        public async Task CreateAsync_ValidReview_ShouldReturnCreatedReview()
        {
            CreateReviewRequest request = new CreateReviewRequest
            {
                ProductId = Guid.NewGuid(),
                Content = "Great Product",
                Rating = 5
            };

            authServiceMock.Setup(a => a.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            reviewRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Review>())).ReturnsAsync(new Review { Id = Guid.NewGuid() });
            userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User
            {
                Names = "Jane Doe",
                Email = null,
                Phone = null
            });

            ReviewResponse result = await reviewService.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Great Product", result.Content);
            Assert.Equal(5, result.Rating);
        }

        [Fact]
        public async Task CreateAsync_Review_ShouldRecalculateProductRating()
        {
            CreateReviewRequest request = new CreateReviewRequest
            {
                ProductId = Guid.NewGuid(),
                Content = "Excellent Product",
                Rating = 4
            };

            authServiceMock.Setup(a => a.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            reviewRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Review>())).ReturnsAsync(new Review { Id = Guid.NewGuid() });
            userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new User
            {
                Names = "John Smith",
                Email = null,
                Phone = null
            });
            productRepositoryMock.Setup(p => p.UpdateRatingAsync(It.IsAny<Guid>(), It.IsAny<double>())).Returns(Task.CompletedTask);

            await reviewService.CreateAsync(request);

            productRepositoryMock.Verify(p => p.UpdateRatingAsync(It.IsAny<Guid>(), It.IsAny<double>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReviewNotFound_ShouldThrowNotFound()
        {
            reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Review)null);

            await Assert.ThrowsAsync<AppException>(() => reviewService.DeleteAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteAsync_UserNotOwner_ShouldThrowForbidden()
        {
            Guid reviewId = Guid.NewGuid();
            Review review = new Review { Id = reviewId, UserId = Guid.NewGuid() };
            reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(review);
            authServiceMock.Setup(a => a.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());

            await Assert.ThrowsAsync<AppException>(() => reviewService.DeleteAsync(reviewId));
        }

        [Fact]
        public async Task DeleteAsync_ValidReview_ShouldDeleteReview()
        {
            Guid reviewId = Guid.NewGuid();
            Review review = new Review { Id = reviewId, UserId = Guid.NewGuid() };
            reviewRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(review);
            authServiceMock.Setup(a => a.GetCurrentUserId()).ReturnsAsync(review.UserId.ToString());
            reviewRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            bool result = await reviewService.DeleteAsync(reviewId);

            Assert.True(result);
        }

        [Fact]
        public async Task SearchReviewsAsync_ValidRequest_ShouldReturnPaginatedReviews()
        {
            SearchReviewsRequest request = new SearchReviewsRequest
            {
                PageNumber = 1,
                PageSize = 10,
                ProductId = default
            };

            reviewRepositoryMock.Setup(r => r.SearchAsync(It.IsAny<Filter<Review>>())).ReturnsAsync(new Paginated<Review>
            {
                Items = new List<Review> { new Review { Id = Guid.NewGuid(), Content = "Amazing!" } },
                TotalCount = 1
            });

            Paginated<ReviewResponse> result = await reviewService.SearchReviewsAsync(request);

            Assert.NotNull(result);
            Assert.Single(result.Items);
        }
    }
}
