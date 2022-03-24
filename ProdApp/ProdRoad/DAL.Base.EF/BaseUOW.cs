using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF;

public abstract class BaseUOW<TDbContext> : IUnitOfWork
where TDbContext : DbContext
{
    protected readonly TDbContext UOWDbContext;

    public BaseUOW(TDbContext uowDbContext)
    {
        UOWDbContext = uowDbContext;
    }
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return UOWDbContext.SaveChanges();
    }
    
    private readonly Dictionary<Type, object> _repoCache = new();
    public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        where TRepository : class
    {
        if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
        {
            return (TRepository) repo;
        }

        var repoInstance = repoCreationMethod();
        _repoCache.Add(typeof(TRepository), repoInstance);
        return repoInstance;
    }

}