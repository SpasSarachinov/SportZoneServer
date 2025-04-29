using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Repositories;

namespace SportZoneServer.Tests.Unit.Repositories;


public class WishlistRepositoryTests : RepositoryTestGenerics, IDisposable
{
    private static ApplicationDbContext _context;
    private readonly WishlistRepository _repository;

    public WishlistRepositoryTests() : base(_context)
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new(options);
        _repository = new(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task IsProductAlreadyInWishlist_ShouldReturnTrue_WhenProductIsInWishlist()
    {
        // Arrange
        Guid productId = Guid.NewGuid();
        Guid wishlistId = Guid.NewGuid();
        WishlistItem wishlistItem = new() { ProductId = productId, Id = wishlistId, IsDeleted = false };
        _context.WishlistItems.Add(wishlistItem);
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.IsProductAlreadyInWishlist(productId, wishlistId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsProductAlreadyInWishlist_ShouldReturnFalse_WhenProductIsNotInWishlist()
    {
        // Arrange
        Guid productId = Guid.NewGuid();
        Guid wishlistId = Guid.NewGuid();

        // Act
        bool result = await _repository.IsProductAlreadyInWishlist(productId, wishlistId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllByUserIdAsync_ShouldReturnWishlistItems_WhenWishlistItemsExist()
    {
        await ClearDatabaseAsync<Product>();
        await ClearDatabaseAsync<User>();
        
        // Arrange
        User user = new() { Email = "<EMAIL>", Names = "Test", Phone = "123456789", PasswordHash = "<PASSWORD>" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        Guid userId = user.Id;

        Product product = new() { Title = "Test Product", Quantity = 10, Description = "Description for test product", MainImageUrl = "http://example.com/image.jpg" };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        WishlistItem wishlistItem = new() { UserId = userId, ProductId = product.Id, IsDeleted = false };
        _context.WishlistItems.Add(wishlistItem);
        await _context.SaveChangesAsync();

        // Act
        ICollection<WishlistItem> result = await _repository.GetAllByUserIdAsync(userId);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetAllByUserIdAsync_ShouldReturnEmpty_WhenNoWishlistItemsExist()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act
        ICollection<WishlistItem> result = await _repository.GetAllByUserIdAsync(userId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetWishlistItemIdAsync_ShouldReturnWishlistItemId_WhenItemExistsInWishlist()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();
        WishlistItem wishlistItem = new() { UserId = userId, ProductId = productId, IsDeleted = false };
        _context.WishlistItems.Add(wishlistItem);
        await _context.SaveChangesAsync();

        // Act
        Guid? result = await _repository.GetWishlistItemIdAsync(userId, productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(wishlistItem.Id, result);
    }

    [Fact]
    public async Task GetWishlistItemIdAsync_ShouldReturnNull_WhenItemDoesNotExistInWishlist()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();

        // Act
        Guid? result = await _repository.GetWishlistItemIdAsync(userId, productId);

        // Assert
        Assert.Null(result);
    }
}
