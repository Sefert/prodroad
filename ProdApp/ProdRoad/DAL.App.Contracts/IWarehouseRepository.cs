using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IWarehouseRepository : IEntityRepository<DAL.App.DTO.Warehouse>, 
    IWarehouseRepositoryCustom<DAL.App.DTO.Warehouse>
{
}

public interface IWarehouseRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}