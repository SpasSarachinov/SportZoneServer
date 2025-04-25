using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;

namespace SportZoneServer.Data.Repositories;

public class ImageRepository(ApplicationDbContext context) : Repository<Image>(context), IImageRepository
{
    
}
