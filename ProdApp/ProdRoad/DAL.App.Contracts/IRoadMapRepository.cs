using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IRoadMapRepository : IEntityRepository<DAL.App.DTO.RoadMap>
{
    Task<IEnumerable<DAL.App.DTO.RoadMap>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DAL.App.DTO.RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}