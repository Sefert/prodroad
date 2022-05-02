using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IPriceGroupRepository : IEntityRepository<DAL.App.DTO.PriceGroup>,
    IPriceGroupRepositoryCustom<DAL.App.DTO.PriceGroup>
{
    
}
public interface IPriceGroupRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}