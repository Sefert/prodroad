using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IRoadMapRepository : IEntityRepository<DTO.App.RoadMap>
{
    Task<IEnumerable<DTO.App.RoadMap>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DTO.App.RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}