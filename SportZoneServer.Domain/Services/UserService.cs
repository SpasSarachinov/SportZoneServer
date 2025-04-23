using SportZoneServer.Common.Requests.Auth;
using SportZoneServer.Common.Requests.Users;
using SportZoneServer.Common.Responses.Auth;
using SportZoneServer.Common.Responses.Users;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<IEnumerable<UserResponse>?> GetAsync()
    {
        IEnumerable<User> users = await userRepository.GetAllAsync();

        return users.Select(user => new UserResponse()
        {
            Id = user.Id,
            Email = user.Email,
            Names = user.Names,
            Phone = user.Phone,
            Role = user.Role,
            
        });
    }
    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        User? user = await userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new AppException("User not found.").SetStatusCode(404);
        }
        return new()
        {
            Id = user.Id,
            Email = user.Email,
            Names = user.Names,
            Phone = user.Phone,
            Role = user.Role,
        };    }

    public async Task<UserResponse?> UpdateAsync(UpdateUserRequest request)
    {
        User userBeforeUpdate = (await userRepository.GetByIdAsync(request.Id))!;

        User? updatedUser = new()
        {
            Id = request.Id,
            Email = request.Email,
            Names = request.Names,
            Phone = request.Phone,
            PasswordHash = userBeforeUpdate.PasswordHash,
        };
        
        updatedUser = await userRepository.UpdateAsync(updatedUser);

        return new()
        {
            Id = updatedUser!.Id,
            Email = updatedUser.Email,
            Names = updatedUser.Names,
            Phone = updatedUser.Phone,
            Role = updatedUser.Role,
        };    
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        UserResponse user = (await GetByIdAsync(id))!;

        if (!await userRepository.DeleteAsync(user.Id))
        {
            return false;
        }

        return true;    
    }
}
