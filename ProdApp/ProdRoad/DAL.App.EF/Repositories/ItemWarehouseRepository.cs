using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ItemWarehouseRepository : BaseEntityRepository<DAL.App.DTO.ItemWarehouse, Domain.App.ItemWarehouse, AppDbContext>, IItemWarehouseRepository
{
    public ItemWarehouseRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.ItemWarehouse, Domain.App.ItemWarehouse> mapper) : base(dbContext, mapper)
    {
    }
}