using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<AppDbContext,UserTeam,UserTeam>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext, IDalMapper<UserTeam, UserTeam> dalMapper) : base(dbContext, dalMapper)
    {
    }
}