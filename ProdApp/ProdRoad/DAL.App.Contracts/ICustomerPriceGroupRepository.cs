using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ICustomerPriceGroupRepository : IEntityRepository<DAL.App.DTO.CustomerPriceGroup>, 
    ICustomerPriceGroupRepositoryCustom<DAL.App.DTO.CustomerPriceGroup>
{
    
}

public interface ICustomerPriceGroupRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}