using SportZoneServer.Data.Entities;

namespace SportZoneServer.Data.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> IsNameAlreadyUsed(string name);
}
