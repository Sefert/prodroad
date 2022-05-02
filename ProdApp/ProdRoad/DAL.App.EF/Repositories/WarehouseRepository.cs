using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class WarehouseRepository : BaseEntityRepository<DAL.App.DTO.Warehouse, Domain.App.Warehouse, AppDbContext>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Warehouse, Domain.App.Warehouse> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.Warehouse>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.Warehouse?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}