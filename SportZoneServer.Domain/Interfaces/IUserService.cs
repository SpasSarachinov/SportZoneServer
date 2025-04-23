using SportZoneServer.Common.Requests.Users;
using SportZoneServer.Common.Responses.Users;

namespace SportZoneServer.Domain.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>?> GetAsync();
    Task<UserResponse?> GetByIdAsync(Guid id);
    Task<UserResponse?> UpdateAsync(UpdateUserRequest request);
    Task<bool> DeleteAsync(Guid id);
}
