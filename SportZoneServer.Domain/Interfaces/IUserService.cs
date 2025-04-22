using SportZoneServer.Common.Responses.User;

namespace SportZoneServer.Domain.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>?> GetAsync();
}
