using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ItemWarehouseRepository : BaseEntityRepository<DAL.App.DTO.ItemWarehouse, Domain.App.ItemWarehouse, AppDbContext>, IItemWarehouseRepository
{
    public ItemWarehouseRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.ItemWarehouse, Domain.App.ItemWarehouse> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.ItemWarehouse>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.ItemWarehouse?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}