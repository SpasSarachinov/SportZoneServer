using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using Xunit;
using SportZoneServer.Data.Repositories;

namespace SportZoneServer.Tests.Unit.Repositories;

public class ReviewRepositoryTests
{
    private readonly ReviewRepository _repository;
    private readonly ApplicationDbContext _context;

    public ReviewRepositoryTests()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new(options);
        _repository = new(_context);
    }

    [Fact]
    public async Task GetReviews_ShouldReturnReviews_WhenReviewsExistForProduct()
    {
        // Arrange
        Guid productId = Guid.NewGuid();
        Review review1 = new() { ProductId = productId, Content = "Great product!", Rating = 5 };
        Review review2 = new() { ProductId = productId, Content = "Not bad", Rating = 3 };
        _context.Reviews.Add(review1);
        _context.Reviews.Add(review2);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Review> result = await _repository.GetReviews(productId);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetReviews_ShouldReturnEmpty_WhenNoReviewsExistForProduct()
    {
        // Arrange
        Guid productId = Guid.NewGuid();

        // Act
        IEnumerable<Review> result = await _repository.GetReviews(productId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetReviews_ShouldNotReturnDeletedReviews()
    {
        // Arrange
        Guid productId = Guid.NewGuid();
        Review review1 = new() { ProductId = productId, Content = "Great product!", Rating = 5, IsDeleted = true };
        Review review2 = new() { ProductId = productId, Content = "Not bad", Rating = 3 };
        _context.Reviews.Add(review1);
        _context.Reviews.Add(review2);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Review> result = await _repository.GetReviews(productId);

        // Assert
        Assert.Single(result);
        Assert.Equal("Not bad", result.First().Content);
    }
}
