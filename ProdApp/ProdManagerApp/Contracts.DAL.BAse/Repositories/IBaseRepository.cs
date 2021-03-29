using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.BAse.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : class, IDomainEntityId/*, IDomainEntityDate, IDomainEntityDateTime, IDomainEntityTime*/
    {
    }
    public interface IBaseRepository<TEntity, TKey>
        where TKey : IEquatable<TKey> //id.equals(someotherId), like id == someotherId
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId=default,bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId=default, bool noTracking = true);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity, TKey? userId);
        Task<TEntity> RemoveAsync(TKey id, TKey? userId=default);
        Task<bool> ExistsAsync(TKey id, TKey? userId=default);
    }
}