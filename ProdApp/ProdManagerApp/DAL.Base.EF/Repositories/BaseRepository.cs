using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Contracts.DAL.BAse.Repositories;
using Contracts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, Guid>, IBaseRepository<TEntity>
        where TEntity : class, IDomainEntityId
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
    
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;
        public BaseRepository(DbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = dbContext.Set<TEntity>();
        }

        private IQueryable<TEntity> CreateQuery(TKey? userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable(); // add here id options, for data access

            if (userId != null && typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                query = query.Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
            }

            return noTracking ? query.AsNoTracking() : query;
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId, bool noTracking)
        {
            return await CreateQuery(userId, noTracking).ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool noTracking = true)
        {
            return await CreateQuery(userId, noTracking).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity, TKey? userId)
        {
            if (userId != null && typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TEntity)) && 
                !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad entity id to be deleted!");
                //TODO: load entity from db and check the id in entity is correct
            }
            return RepoDbSet.Remove(entity).Entity;
        }

        public virtual async Task<TEntity> RemoveAsync(TKey id, TKey? userId)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null) throw new NullReferenceException($"Entity with id {id} not found.");
            return Remove(entity!, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId)
        {
            //TODO:hide id
            if (userId != null && !typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TEntity)))
            {
                throw new AuthenticationException($"Bad user id inside entity.! user with id:{userId} and ref id:{id} ");
            }
            return await RepoDbSet.AnyAsync(e =>
                e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }
    }
}