using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ICustomerRepository : IEntityRepository<DAL.App.DTO.Customer>, 
    ICustomerRepositoryCustom<DAL.App.DTO.Customer>
{

}

public interface ICustomerRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}