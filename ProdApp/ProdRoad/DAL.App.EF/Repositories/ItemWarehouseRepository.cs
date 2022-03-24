using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ItemWarehouseRepository : BaseEntityRepository<ItemWarehouse, AppDbContext>, IItemWarehouseRepository
{
    public ItemWarehouseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}