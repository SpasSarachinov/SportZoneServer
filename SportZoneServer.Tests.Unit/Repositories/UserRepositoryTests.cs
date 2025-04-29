using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Repositories;
using Xunit;

namespace SportZoneServer.Tests.Unit.Repositories;

public class UserRepositoryTests : RepositoryTestGenerics
{
    private static ApplicationDbContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests() : base(_context)
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new(options);
        _repository = new(_context);
    }

    [Fact]
    public async Task IsEmailAlreadyUsed_ShouldReturnTrue_WhenEmailIsUsed()
    {
        await ClearDatabaseAsync<User>();

        // Arrange
        string email = "test@example.com";
        User existingUser = new() { Email = email, IsDeleted = false, Names = "test", Phone = "test", PasswordHash = "test" };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.IsEmailAlreadyUsed(email);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailAlreadyUsed_ShouldReturnFalse_WhenEmailIsNotUsed()
    {
        await ClearDatabaseAsync<User>();
        
        // Arrange
        string email = "test@example.com";

        // Act
        bool result = await _repository.IsEmailAlreadyUsed(email);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsEmailAlreadyUsed_ShouldReturnFalse_WhenUserIsDeleted()
    {
        await ClearDatabaseAsync<User>();

        // Arrange
        string email = "test@example.com";
        User existingUser = new() { Email = email, IsDeleted = false, Names = "test", Phone = "test", PasswordHash = "test" };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();
        
        existingUser.IsDeleted = true;
        await _context.SaveChangesAsync();

        // Act
        bool result = await _repository.IsEmailAlreadyUsed(email);

        // Assert
        Assert.False(result);
    }
}
