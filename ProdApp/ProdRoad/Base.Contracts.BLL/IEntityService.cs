using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.Contracts.BLL;

public interface IEntityService<TEntity> : IEntityRepository<TEntity>, IEntityService<TEntity, Guid>
where TEntity: class, IBaseEntity
{
    
}
public interface IEntityService<TEntity, TKey> : IEntityRepository<TEntity, TKey>
    where TEntity: class, IBaseEntity<TKey>
    where TKey: IEquatable<TKey>
{
    
}