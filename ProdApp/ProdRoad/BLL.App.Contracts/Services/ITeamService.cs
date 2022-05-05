using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface ITeamService : IEntityService<BLL.App.DTO.Team>,
    ITeamRepositoryCustom<BLL.App.DTO.Team>
{
    Task<IEnumerable<BLL.App.DTO.Team>> GetAllAsync(Guid userId, bool noTracking = true);

    Task<BLL.App.DTO.Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);

    Task<BLL.App.DTO.Team?> PublicTeamAsync(string code, bool noTracking = true);
}

