using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity: class, IBaseEntity
{
}

public interface IEntityRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    //TODO: check user ownership
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
    TEntity Remove(TKey id);
    
    TEntity? FirstOrDefault(TKey id, bool noTracking = true);
    IEnumerable<TEntity> GetAll(bool noTracking = true);
    bool Exists(TKey id);
    void ModifyState(TEntity entity);
    
    
    //async 
    
    Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllAsync (bool noTracking = true);
    Task<bool> ExistsAsync (TKey id);
    Task<TEntity> RemoveAsync (TKey id);
} 