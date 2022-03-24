using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class WarehouseRepository : BaseEntityRepository<Warehouse, AppDbContext>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}