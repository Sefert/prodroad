using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseEntityRepository<AppDbContext,Team>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}