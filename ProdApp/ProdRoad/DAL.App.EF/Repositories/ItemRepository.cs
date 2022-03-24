using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ItemRepository : BaseEntityRepository<Item, AppDbContext>, IItemRepository
{
    public ItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}