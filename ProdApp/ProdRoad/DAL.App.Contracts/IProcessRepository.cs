using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IProcessRepository : IEntityRepository<DAL.App.DTO.Process>,
    IProcessRepositoryCustom<DAL.App.DTO.Process>
{
    
}

public interface IProcessRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}