using Public.App.DTO.v1;

namespace Public.App.Contracts.v1.Services;

public interface ITeamService
{
    Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true);

    Task<Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}