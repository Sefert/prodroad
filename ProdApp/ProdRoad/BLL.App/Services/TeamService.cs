using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;
using Domain.Base;

namespace BLL.App.Services;

public class TeamService :
    BaseEntityService<Team, DAL.App.DTO.Team, ITeamRepository>, 
    ITeamService
{
    public TeamService(
        ITeamRepository repo, 
        IMapper<Team, DAL.App.DTO.Team> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = false)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,id,noTracking));
    }
    
    public async Task<Team?> PublicTeamAsync(LangStr code, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.PublicTeamAsync(code,id,noTracking));
    }
}