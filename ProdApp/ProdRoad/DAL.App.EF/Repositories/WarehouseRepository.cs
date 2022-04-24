using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class WarehouseRepository : BaseEntityRepository<DTO.App.Warehouse, Domain.App.Warehouse, AppDbContext>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext dbContext, IMapper<DTO.App.Warehouse, Domain.App.Warehouse> mapper) : base(dbContext, mapper)
    {
    }
}