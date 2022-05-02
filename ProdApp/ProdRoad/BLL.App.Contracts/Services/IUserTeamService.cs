using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IUserTeamService : IEntityService<BLL.App.DTO.UserTeam>, 
    IUserTeamRepositoryCustom<BLL.App.DTO.UserTeam>
{
    
}