using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseEntityRepository<AppDbContext,Team,Team>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext) : base(dbContext, new DalMapper<Team, Team>())
    {
    }
}