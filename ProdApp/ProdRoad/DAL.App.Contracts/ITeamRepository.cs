using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ITeamRepository : IEntityRepository<DAL.App.DTO.Team>
{
    Task<IEnumerable<DAL.App.DTO.Team>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DAL.App.DTO.Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}