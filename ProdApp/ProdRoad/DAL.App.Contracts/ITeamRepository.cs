using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ITeamRepository : IEntityRepository<DTO.App.Team>
{
    Task<IEnumerable<DTO.App.Team>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DTO.App.Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}