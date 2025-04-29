using System.Threading.Tasks;
using Xunit;
using SportZoneServer.Data.Repositories;
using SportZoneServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data;

namespace SportZoneServer.Tests.Unit.Repositories;

public class CategoryRepositoryTests
{
    private readonly CategoryRepository _repository;
    private readonly ApplicationDbContext _context;

    public CategoryRepositoryTests()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "CategoryRepositoryTestsDb")
            .Options;
        _context = new ApplicationDbContext(options);
        _repository = new CategoryRepository(_context);
    }

    [Fact]
    public async Task IsNameAlreadyUsed_ShouldReturnTrue_WhenNameIsUsed()
    {
        // Arrange
        string name = "test@example.com";
        User existingUser = new User { Email = name, IsDeleted = false, Names = "test", Phone = "test", PasswordHash = "test" };
        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.IsNameAlreadyUsed(name);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsNameAlreadyUsed_ShouldReturnFalse_WhenNameIsNotUsed()
    {
        // Arrange
        string name = "test@example.com";

        // Act
        bool result = await _repository.IsNameAlreadyUsed(name);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsNameAlreadyUsed_ShouldReturnFalse_WhenUserIsDeleted()
    {
        // Arrange
        string name = "test@example.com";
        User existingUser = new User { Email = name, IsDeleted = true, Names = "test", Phone = "test", PasswordHash = "test" };
        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.IsNameAlreadyUsed(name);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsNameAlreadyUsed_ShouldReturnFalse_WhenNameIsNullOrEmpty()
    {
        // Arrange
        string name = "";

        // Act
        bool result = await _repository.IsNameAlreadyUsed(name);

        // Assert
        Assert.False(result);
    }
}
