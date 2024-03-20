using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity>
    where TEntity : class, IDomainEntityId
{
    
}
public interface IEntityRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IDomainEntityId<TKey>
{
    bool Exists(TKey id, TKey? userId = default);
    Task<bool> ExistsAsync(TKey id, TKey? userId = default);
    
    IEnumerable<TEntity> GetAll(TKey? userId = default, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true);
    
    TEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true);
    
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    
    int Remove(TKey id, TKey? userId = default);
    int Remove(TEntity entity, TKey userId = default);
    Task<int> RemoveAsync(TEntity entity, TKey? userId = default);
    Task<int> RemoveAsync(TKey id, TKey? userId = default);
    Task<int> SaveChangesAsync();
}