using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ICustomerPriceRepository : IEntityRepository<DAL.App.DTO.CustomerPrice>, 
    ICustomerPriceRepositoryCustom<DAL.App.DTO.CustomerPrice>
{
    
}
public interface ICustomerPriceRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}