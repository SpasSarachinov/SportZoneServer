using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Repositories;
using Xunit;

namespace SportZoneServer.Tests.Unit.Repositories;

public class OrderItemRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly OrderItemRepository _repository;

    public OrderItemRepositoryTests()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "OrderItemRepositoryTestsDb")
            .Options;

        _context = new(options);
        _repository = new(_context);
    }

    [Fact]
    public async Task AddRange_ShouldReturnFalse_WhenOrderItemsIsNull()
    {
        // Arrange
        ICollection<OrderItem>? orderItems = null;

        // Act
        bool result = await _repository.AddRange(orderItems);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddRange_ShouldReturnFalse_WhenOrderItemsIsEmpty()
    {
        // Arrange
        ICollection<OrderItem> orderItems = new List<OrderItem>();

        // Act
        bool result = await _repository.AddRange(orderItems);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddRange_ShouldReturnTrue_WhenOrderItemsAreValid()
    {
        // Arrange
        ICollection<OrderItem> orderItems = new List<OrderItem>
        {
            new()
            {
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2,
                SinglePrice = 100m,
                TotalPrice = 200m,
                Title = "Sample Product",
                PrimaryImageUri = "http://example.com/image.jpg"
            }
        };

        // Act
        bool result = await _repository.AddRange(orderItems);

        // Assert
        Assert.True(result);

        List<OrderItem> addedOrderItems = _context.OrderItems.ToList();
        Assert.Single(addedOrderItems);
        Assert.Equal(orderItems.First().Title, addedOrderItems.First().Title);
        Assert.Equal(orderItems.First().TotalPrice, addedOrderItems.First().TotalPrice);
    }
}
