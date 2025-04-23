using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsEmailAlreadyUsed(string email);
    }
}
