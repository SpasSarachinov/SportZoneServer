using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Core.Pages;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Data.Repositories;
using Xunit;

namespace SportZoneServer.Tests.Unit.Repositories;

public class RepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly Repository<Image> _repository;

    public RepositoryTests()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new Repository<Image>(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity_WhenEntityIsValid()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        };

        // Act
        Image? result = await _repository.AddAsync(entity);

        // Assert
        Assert.Equal(entity, result);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEntities_WhenEntitiesExist()
    {
        // Arrange
        List<Image> entities = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Uri = "test"
            }
        };
        _context.Images.AddRange(entities);
        await _context.SaveChangesAsync();

        // Act
        IEnumerable<Image> result = await _repository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        };
        _context.Images.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        Image? result = await _repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenEntityIsDeleted()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            IsDeleted = true,
            Uri = "test"
        };
        _context.Images.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        Image? result = await _repository.GetByIdAsync(entity.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenEntityIsDeletedSuccessfully()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        };
        _context.Images.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.DeleteAsync(entity.Id);

        // Assert
        Assert.True(result);
        await _context.SaveChangesAsync(); // Ensure the delete is persisted
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenEntityNotFound()
    {
        // Arrange
        _context.Images.Add(new Image
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        });
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedEntity_WhenEntityExists()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        };
        _context.Images.Add(entity);
        await _context.SaveChangesAsync();

        // Act
        Image? result = await _repository.UpdateAsync(entity);

        // Assert
        Assert.Equal(entity, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenEntityDoesNotExist()
    {
        // Arrange
        Image entity = new()
        {
            Id = Guid.NewGuid(),
            Uri = "test"
        };

        // Act
        Image? result = await _repository.UpdateAsync(entity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnPaginatedResults_WhenValidRequest()
    {
        // Arrange
        Filter<Image> filter = new()
        {
            Predicate = x => x.Uri.Contains("test"),
            PageNumber = 1,
            PageSize = 10,
            SortBy = "CreatedOn",
            SortDescending = false,
        };

        List<Image> items = new()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Uri = "test",
                CreatedOn = DateTime.UtcNow
            }
        };

        _context.Images.AddRange(items);
        await _context.SaveChangesAsync();

        // Act
        Paginated<Image> result = await _repository.SearchAsync(filter);

        // Assert
        Assert.Equal(items.Count, result.TotalCount);
    }
}
