using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IItemRepository : IEntityRepository<DAL.App.DTO.Item>, IItemRepositoryCustom<DAL.App.DTO.Item>
{
}

public interface IItemRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}