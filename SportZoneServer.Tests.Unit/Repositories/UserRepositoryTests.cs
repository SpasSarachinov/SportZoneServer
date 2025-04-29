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

        string email = "test@example.com";
        User existingUser = new()
        {
            Email = email,
            IsDeleted = false,
            Names = "test",
            Phone = "test",
            PasswordHash = "test"
        };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        bool result = await _repository.IsEmailAlreadyUsed(email);

        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailAlreadyUsed_ShouldReturnFalse_WhenEmailIsNotUsed()
    {
        await ClearDatabaseAsync<User>();
        
        string email = "test@example.com";

        bool result = await _repository.IsEmailAlreadyUsed(email);

        Assert.False(result);
    }

    [Fact]
    public async Task IsEmailAlreadyUsed_ShouldReturnFalse_WhenUserIsDeleted()
    {
        await ClearDatabaseAsync<User>();

        string email = "test@example.com";
        User existingUser = new()
        {
            Email = email,
            IsDeleted = false,
            Names = "test",
            Phone = "test",
            PasswordHash = "test"
        };
        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();
        
        existingUser.IsDeleted = true;
        await _context.SaveChangesAsync();

        bool result = await _repository.IsEmailAlreadyUsed(email);

        Assert.False(result);
    }
}
