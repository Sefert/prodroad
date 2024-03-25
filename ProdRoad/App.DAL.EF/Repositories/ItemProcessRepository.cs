using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ItemProcessRepository : BaseEntityRepository<AppDbContext,ItemProcess,ItemProcess>, IItemProcessRepository
{
    public ItemProcessRepository(AppDbContext dbContext) : base(dbContext, new DalMapper<ItemProcess, ItemProcess>())
    {
    }
}