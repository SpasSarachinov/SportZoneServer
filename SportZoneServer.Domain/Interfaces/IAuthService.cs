using SportZoneServer.Common.Requests.Auth;
using SportZoneServer.Common.Responses.Auth;

namespace SportZoneServer.Domain.Interfaces;

public interface IAuthService
{
    Task<RegisterUserResponse?> RegisterAsync(RegisterUserRequest request);
    Task<TokenResponse?> LoginAsync(LoginUserRequest request);
    Task<TokenResponse?> RefreshTokensAsync(RefreshTokenRequest request);
}
