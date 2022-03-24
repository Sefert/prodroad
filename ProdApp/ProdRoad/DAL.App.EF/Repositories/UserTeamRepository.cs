using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<UserTeam, AppDbContext>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}