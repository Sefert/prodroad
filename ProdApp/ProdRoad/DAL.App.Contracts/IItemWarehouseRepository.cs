using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IItemWarehouseRepository : IEntityRepository<DAL.App.DTO.ItemWarehouse>,
    IItemWarehouseRepositoryCustom<DAL.App.DTO.ItemWarehouse>
{
    
}
public interface IItemWarehouseRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}