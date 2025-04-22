using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}