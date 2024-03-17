using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PriceRepository : BaseEntityRepository<AppDbContext,Price,Price>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IDalMapper<Price, Price> dalMapper) : base(dbContext, dalMapper)
    {
    }
}