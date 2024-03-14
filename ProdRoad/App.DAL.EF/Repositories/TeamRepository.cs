using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TeamRepository : BaseEntityRepository<Guid,AppDbContext,Team>
{
    public TeamRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}