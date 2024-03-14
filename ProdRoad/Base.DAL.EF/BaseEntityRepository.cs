using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository
{
    public BaseEntityRepository(DbContext dbContext)
    {
        
    }
}

public class BaseEntityRepository<TKey, TDbContext, TDomainEntity, TDalEntity>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
    where TDomainEntity : class, IDomainEntityId
    //where TDalEntity : class, IDomainEntityId
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;

    public BaseEntityRepository(TDbContext dbContext)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<TDomainEntity>();
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            query = query
                .Include("AppUser")
                .Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }
        return query;
    }
    
    //EXIST
    public virtual bool Exists(TKey id, TKey userId = default)
    {
        return CreateQuery(userId).Any(e => e.Id.Equals(id));
    }
    public virtual async Task<bool> ExistsAsync(TKey id, TKey userId = default)
    {
        return await CreateQuery(userId).AnyAsync(e => e.Id.Equals(id));
    }
    
    //GET-ALL
    public virtual IEnumerable<TDomainEntity> GetAll(TKey userId = default, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).ToList();
    }
    public virtual async Task<IEnumerable<TDomainEntity>> GetAllAsync(TKey userId = default, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking).ToListAsync();
    }
    
    //GET-FIRST
    public TDomainEntity? FirstOrDefault(TKey id, TKey userId = default, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).FirstOrDefault(m => m.Id.Equals(id));
    }

    public async Task<TDomainEntity?> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking).FirstOrDefaultAsync(m => m.Id.Equals(id));
    }
    
    //ADD
    public virtual TDomainEntity Add(TDomainEntity entity)
    {
        return RepoDbSet.Add(entity).Entity!;
    }

    //UPDATE
    public virtual TDomainEntity Update(TDomainEntity entity)
    {
        return RepoDbSet.Update(entity).Entity!;
    }
    
    //DELETE
    public virtual int Remove(TKey id, TKey userId = default)
    {
        if (userId == null)
        {
            return RepoDbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteDelete();
        }

        return CreateQuery(userId)
            .Where(e => e.Id.Equals(id))
            .ExecuteDelete();
    }
    public virtual async Task<int> RemoveAsync(TDomainEntity entity, TKey userId = default)
    {
        if (userId == null)
        {
            return await RepoDbSet.Where(e => e.Id.Equals(entity.Id)).ExecuteDeleteAsync();
        }

        return await CreateQuery(userId)
            .Where(e => e.Id.Equals(entity.Id))
            .ExecuteDeleteAsync();
    }
    public virtual async Task<int> RemoveAsync(TKey id, TKey userId = default)
    {
        if (userId == null)
        {
            return await RepoDbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        return await CreateQuery(userId)
            .Where(e => e.Id.Equals(id))
            .ExecuteDeleteAsync();
    }
}
