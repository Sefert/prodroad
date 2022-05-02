using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface ITeamService : IEntityService<BLL.App.DTO.Team>,
    ITeamRepositoryCustom<BLL.App.DTO.Team>
{
    
}