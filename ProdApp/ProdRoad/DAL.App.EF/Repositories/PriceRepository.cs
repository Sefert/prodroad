using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class PriceRepository : BaseEntityRepository<Price, AppDbContext>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}