using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ItemRepository : BaseEntityRepository<AppDbContext,Item,Item>, IItemRepository
{
    public ItemRepository(AppDbContext dbContext, IDalMapper<Item, Item> dalMapper) : base(dbContext, dalMapper)
    {
    }
}