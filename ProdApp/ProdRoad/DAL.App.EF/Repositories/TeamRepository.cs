using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class TeamRepository : BaseEntityRepository<Team, AppDbContext>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}