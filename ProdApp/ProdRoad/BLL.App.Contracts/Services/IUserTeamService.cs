using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IUserTeamService : IEntityService<BLL.App.DTO.UserTeam>, 
    IUserTeamRepositoryCustom<BLL.App.DTO.UserTeam>
{
    Task<IEnumerable<BLL.App.DTO.UserTeam>> GetAllAsync(Guid userId, bool noTracking = true);

    Task<BLL.App.DTO.UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}