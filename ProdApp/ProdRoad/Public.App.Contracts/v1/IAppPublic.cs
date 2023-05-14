using Public.App.Contracts.v1.Services;

namespace Public.App.Contracts.v1;

public interface IAppPublic
{
    Task<int> SaveChangesAsync();
    IUserTeamService UserTeams { get; }
    ITeamService Teams { get; }
}