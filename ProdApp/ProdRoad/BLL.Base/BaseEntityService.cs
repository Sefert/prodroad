using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace BLL.Base;

public class BaseEntityService<TBllEntity, TDalEntity, TRepository>
    : BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>, IEntityService<TBllEntity>
    where TDalEntity: class, IBaseEntity
    where TBllEntity: class, IBaseEntity
    where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repo, IMapper<TBllEntity, TDalEntity> mapper) : base(repo, mapper)
    {
    }
}


// out goes and in comes the bll entity and is mapped into dal entity
public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
    where TRepository : IEntityRepository<TDalEntity,TKey>
    where TDalEntity : class, IBaseEntity<TKey>
{
    protected TRepository Repo;
    protected IMapper<TBllEntity, TDalEntity> Mapper;
    
    public BaseEntityService(TRepository repo, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repo = repo;
        Mapper = mapper;
    }
    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repo.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repo.Update(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(Repo.Remove(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TKey id)
    {
        return Mapper.Map(Repo.Remove(id))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(Repo.FirstOrDefault(id,noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(bool noTracking = true)
    {
        return Repo.GetAll(noTracking).Select(x => Mapper.Map(x))!;
    }

    public bool Exists(TKey id)
    {
        return Repo.Exists(id);
    }

    //TODO: what was this for
    public void ModifyState(TBllEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(id,noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true)
    {
        return (await Repo.GetAllAsync(noTracking)).Select(x => Mapper.Map(x)!);
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await Repo.ExistsAsync(id);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repo.RemoveAsync(id))!;
    }
}