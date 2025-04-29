using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Core.Enums;
using SportZoneServer.Core.Exceptions;
using Xunit;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Repositories;

namespace SportZoneServer.Tests.Unit.Repositories;

public class OrderRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new(options);
        _repository = new(_context);
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = "<EMAIL>",
            Names = "test",
            Phone = "test",
            PasswordHash = "<PASSWORD>"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        Guid userId = user.Id;

        Order order = new()
        {
            UserId = userId,
            Status = OrderStatus.Created,
            IsDeleted = false,
            Items = new List<OrderItem>
            {
                new()
                {
                    PrimaryImageUri = "http://example.com/image.jpg",
                    Title = "Sample Product",
                    Quantity = 0,
                    SinglePrice = 0,
                    TotalPrice = 0,
                }
            }
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        try
        {
            // Act
            Order? result = await _repository.GetByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(OrderStatus.Created, result.Status);
            Assert.False(result.IsDeleted);
            Assert.NotNull(result.Items);
            Assert.NotNull(result.User);
        }
        finally
        {
            // Cleanup
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act
        Order? result = await _repository.GetByUserIdAsync(userId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByUserIdWithoutStatusRestrictionAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = "<EMAIL>",
            Names = "test",
            Phone = "test",
            PasswordHash = "<PASSWORD>"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        Order order = new()
        {
            UserId = user.Id,
            Status = OrderStatus.Created
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act
        Order? result = await _repository.GetByUserIdWithoutStatusRestrictionAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result?.UserId);
    }

    [Fact]
    public async Task AddAsync_ShouldAddOrder_WhenCalled()
    {
        // Arrange
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = "<EMAIL>",
            Names = "test",
            Phone = "test",
            PasswordHash = "<PASSWORD>"
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        // Act
        Order result = await _repository.AddAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.UserId);
    }

    [Fact]
    public async Task ChangeStatusAsync_ShouldUpdateOrderStatus_WhenOrderExists()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order = new()
        {
            Id = orderId,
            Status = OrderStatus.Created
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act
        Order result = await _repository.ChangeStatusAsync(orderId, OrderStatus.Shipped);

        // Assert
        Assert.Equal(OrderStatus.Shipped, result.Status);
    }

    [Fact]
    public async Task ChangeStatusAsync_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => _repository.ChangeStatusAsync(orderId, OrderStatus.Shipped));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOrder_WhenOrderExists()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order = new()
        {
            Id = orderId,
            Status = OrderStatus.Created
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act
        order.Status = OrderStatus.Shipped;
        Order? result = await _repository.UpdateAsync(order);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(OrderStatus.Shipped, result?.Status);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        Order order = new()
        {
            Id = Guid.NewGuid(),
            Status = OrderStatus.Created
        };

        // Act
        Order? result = await _repository.UpdateAsync(order);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        Guid userId = Guid.NewGuid(); // This user does not exist

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => _repository.AddAsync(userId));
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnNull_WhenOrderIsDeleted()
    {
        // Arrange
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = "<EMAIL>",
            Names = "test",
            Phone = "test",
            PasswordHash = "<PASSWORD>"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        Order order = new()
        {
            UserId = user.Id,
            Status = OrderStatus.Created,
            IsDeleted = true
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act
        Order? result = await _repository.GetByUserIdAsync(user.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ChangeStatusAsync_ShouldThrowException_WhenOrderNotFound_Extended()
    {
        // Arrange
        Guid orderId = Guid.NewGuid(); // This order does not exist

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => _repository.ChangeStatusAsync(orderId, OrderStatus.Shipped));
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenOrderDoesNotExist_Extended()
    {
        // Arrange
        Order order = new()
        {
            Id = Guid.NewGuid(),
            Status = OrderStatus.Created
        };

        // Act
        Order? result = await _repository.UpdateAsync(order);

        // Assert
        Assert.Null(result);
    }

    [Fact(Skip = "Skipping this test for now")]
    public async Task ChangeStatusAsync_ShouldNotAllowInvalidStatusChange()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Order order = new()
        {
            Id = orderId,
            Status = OrderStatus.Created
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => _repository.ChangeStatusAsync(orderId, OrderStatus.Created));
    }
}
