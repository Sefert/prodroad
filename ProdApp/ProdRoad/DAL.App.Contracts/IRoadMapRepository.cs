using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface IRoadMapRepository : IEntityRepository<RoadMap>
{
    Task<IEnumerable<RoadMap>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}