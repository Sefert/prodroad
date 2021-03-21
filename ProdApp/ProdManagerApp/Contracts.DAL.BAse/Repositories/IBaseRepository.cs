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
        where TEntity : class, IDomainEntityId<TKey>/*, IDomainEntityDate<TKey>, IDomainEntityDateTime<TKey>, IDomainEntityTime<TKey>*/
        where TKey : IEquatable<TKey> //id.equals(someotherId), like id == someotherId
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<TEntity> Remove(TKey id);
        Task<bool> ExistsAsync(TKey id);
    }
}