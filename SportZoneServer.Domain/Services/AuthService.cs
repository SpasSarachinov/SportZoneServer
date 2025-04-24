using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportZoneServer.Common.Requests.Auth;
using SportZoneServer.Common.Responses.Auth;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Core.StaticClasses;
using SportZoneServer.Data;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class AuthService(ApplicationDbContext context, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public async Task<RegisterUserResponse?> RegisterAsync(RegisterUserRequest request)
    {
        if (await userRepository.IsEmailAlreadyUsed(request.Email))
        {
            throw new AppException("Email is already in use.").SetStatusCode(409);
        }

        User user = new()
        {
            Email = request.Email,
            PasswordHash = "temporaryPasswordHash",
            Names = request.Names,
            Phone = request.Phone,
            Role = Roles.RegisteredCustomer
        };
        
        string hashedPassword = new PasswordHasher<User>()
            .HashPassword(user, request.Password);

        user.Email = request.Email;
        user.PasswordHash = hashedPassword;

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new()
        {
            Id = user.Id,
        };
    }

    public async Task<TokenResponse?> LoginAsync(LoginUserRequest request)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null)
        {
            return null;
        }
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
            == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    public async Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request)
    {
        User? user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }

        return await CreateTokenResponse(user);
        
    }
    
    private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        User? user = await context.Users.FindAsync(userId);
        if (user is null || user.RefreshToken != refreshToken
                         || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private async Task<TokenResponse> CreateTokenResponse(User? user)
    {
        return new()
        {
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }
    
    private string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        string refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(int.Parse(Environment.GetEnvironmentVariable("REFRESH_TOKEN_EXPIRY_DAYS")!));
        await context.SaveChangesAsync();
        return refreshToken;
    }

    private string CreateToken(User user)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        ];

        SymmetricSecurityKey key = new(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET")!));

        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken tokenDescriptor = new(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("JWT_TOKEN_EXPIRY_MINUTES")!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    
    public string? GetCurrentUserId()
    {
        return GetClaimValue(ClaimTypes.NameIdentifier);
    }

    public string? GetCurrentUserEmail()
    {
        return GetClaimValue(ClaimTypes.Name);
    }

    public string? GetCurrentUserRole()
    {
        return GetClaimValue(ClaimTypes.Role);
    }
    private string? GetClaimValue(string claimType)
    {
        return httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;
    }
}
