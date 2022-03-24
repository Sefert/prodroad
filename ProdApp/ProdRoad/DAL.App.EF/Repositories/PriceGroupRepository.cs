using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class PriceGroupRepository : BaseEntityRepository<PriceGroup, AppDbContext>, IPriceGroupRepository
{
    public PriceGroupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}