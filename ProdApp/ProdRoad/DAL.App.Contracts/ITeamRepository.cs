using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface ITeamRepository : IEntityRepository<Team>
{
    Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}