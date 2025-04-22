using SportZoneServer.Common.Responses.User;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class UserService : IUserService
{
    public Task<IEnumerable<UserResponse>?> GetAsync()
    {
        throw new NotImplementedException();
    }
}
