using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RoadMapRepository : BaseEntityRepository<AppDbContext,RoadMap,RoadMap>, IRoadMapRepository
{
    public RoadMapRepository(AppDbContext dbContext, IDalMapper<RoadMap, RoadMap> dalMapper) : base(dbContext, dalMapper)
    {
    }
}