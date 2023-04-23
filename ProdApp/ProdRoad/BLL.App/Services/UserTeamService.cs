using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class UserTeamService :
    BaseEntityService<UserTeam, DAL.App.DTO.UserTeam, IUserTeamRepository>, 
    IUserTeamService
{
    public UserTeamService(
        IUserTeamRepository repo, 
        IMapper<UserTeam, DAL.App.DTO.UserTeam> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<UserTeam>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,id,noTracking));
    }
    
    /*public async Task<UserTeam?> GetUserTeamByTeamIdAsync(Guid userId, Guid teamId, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,id,noTracking));
    }*/
}