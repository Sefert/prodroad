using Public.App.DTO.v1.UserTeamDTO;

namespace Public.App.Contracts.v1.Services;
public interface IUserTeamService
{
    Task<IEnumerable<UserTeam>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<UserTeam?> FirstOrDefaultAsync(Guid id);
    Task<UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
    void ModifyState(UserTeam entity);
    UserTeam Add(UserTeam entity);
    Task<UserTeam> RemoveAsync(Guid id);
    bool Exists(Guid id);
}