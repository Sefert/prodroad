using Base.Contracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF;

//TODO: do not fetch unneeded data from DB on every request
public class BaseEntityRepository<TAppEntity, TDomainEntity, TDbContext> : BaseEntityRepository<TAppEntity, TDomainEntity,  Guid, TDbContext>
    where TAppEntity : class, IBaseEntity<Guid>
    where TDomainEntity: class, IBaseEntity<Guid>
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IMapper<TAppEntity,TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityRepository<TAppEntity, TDomainEntity, TKey, TDbContext> : IEntityRepository<TAppEntity, TKey>
    where TAppEntity : class, IBaseEntity<Guid>, IBaseEntity<TKey>
    where TDomainEntity: class, IBaseEntity<Guid>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TAppEntity, TDomainEntity> Mapper;
        
    public BaseEntityRepository(TDbContext dbContext, IMapper<TAppEntity,TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        Mapper = mapper;
        RepoDbSet = dbContext.Set<TDomainEntity>();
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(bool noTracking = true)
    {
        //TODO: entity ownership control
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    
    public virtual TAppEntity Add(TAppEntity entity)
    {
        var domainEntity = Mapper.Map(entity);
        return Mapper.Map(RepoDbSet.Add(domainEntity!).Entity)!;
    }

    public virtual TAppEntity Update(TAppEntity entity)
    {
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TAppEntity Remove(TAppEntity entity)
    {
        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TAppEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            //TODO: implement custom exception for entity not found
            throw new NullReferenceException($"Entity {typeof(TAppEntity).Name} with id {id} was not found");
        }
        return Remove(entity);
    }

    public virtual TAppEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(
            CreateQuery(noTracking)
                .FirstOrDefault(a => a.Id.Equals(id))
        );
    }

    public virtual IEnumerable<TAppEntity> GetAll(bool noTracking = true)
    {
        return CreateQuery(noTracking)
            .ToList()
            .Select(x => Mapper.Map(x)!);
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id));
    }

    public void ModifyState(TAppEntity entity)
    {
        RepoDbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task<TAppEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id)));
    }

    public virtual async Task<IEnumerable<TAppEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await CreateQuery(noTracking).ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(a => a.Id.Equals(id));
    }

    public virtual async Task<TAppEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            //TODO: implement custom exception for entity not found
            throw new NullReferenceException($"Entity {typeof(TAppEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }
    
}