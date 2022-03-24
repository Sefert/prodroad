using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class RoadMapRepository : BaseEntityRepository<RoadMap, AppDbContext>, IRoadMapRepository
{
    public RoadMapRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}