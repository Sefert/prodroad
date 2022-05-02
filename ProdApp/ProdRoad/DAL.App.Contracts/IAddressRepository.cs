using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IAddressRepository : IEntityRepository<DAL.App.DTO.Address>, 
    IAddressRepositoryCustom<DAL.App.DTO.Address>
{
}

public interface IAddressRepositoryCustom<TEntity>
where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}