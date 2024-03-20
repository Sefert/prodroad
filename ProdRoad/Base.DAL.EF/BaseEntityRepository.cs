using App.Contracts.DAL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDbContext, TDomainEntity, TDalEntity> : 
    BaseEntityRepository<Guid, TDbContext, TDomainEntity, TDalEntity>, IEntityRepository<TDalEntity>
    where TDbContext : DbContext
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
{
    public BaseEntityRepository(TDbContext dbContext, 
            IDalMapper<TDomainEntity, TDalEntity> dalMapper) : 
            base(dbContext, dalMapper)
    {
    }
}

public class BaseEntityRepository<TKey, TDbContext, TDomainEntity, TDalEntity>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IDalMapper<TDomainEntity, TDalEntity> Mapper;

    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> dalMapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<TDomainEntity>();
        Mapper = dalMapper;
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
    public virtual IEnumerable<TDalEntity> GetAll(TKey userId = default, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).ToList().Select(de => Mapper.MapLR(de))!;
    }
    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey userId = default, bool noTracking = true)
    {
        return (await CreateQuery(userId, noTracking).ToListAsync()).Select(de => Mapper.MapLR(de))!;
    }
    
    //GET-FIRST
    public TDalEntity? FirstOrDefault(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.MapLR(CreateQuery(userId, noTracking).FirstOrDefault(m => m.Id.Equals(id)));
    }

    public async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.MapLR(await CreateQuery(userId, noTracking).FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
    
    //ADD
    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.MapLR(RepoDbSet.Add(Mapper.MapRL(entity)!).Entity)!;
    }

    //UPDATE
    public virtual TDalEntity Update(TDalEntity entity)
    {
        return Mapper.MapLR(RepoDbSet.Update(Mapper.MapRL(entity)!).Entity)!;
    }

    //DELETE
    public virtual int Remove(TDalEntity entity,  TKey userId = default)
    {
        if (userId == null)
        {
            return RepoDbSet
                .Where(e => e.Id.Equals(entity.Id))
                .ExecuteDelete();
        }

        return CreateQuery(userId)
            .Where(e => e.Id.Equals(entity.Id))
            .ExecuteDelete();
    }
    
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
    
    public virtual async Task<int> RemoveAsync(TDalEntity entity, TKey userId = default)
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
    
    public virtual async Task<int> SaveChangesAsync()
    {
        return await RepoDbContext.SaveChangesAsync();
    }
}
