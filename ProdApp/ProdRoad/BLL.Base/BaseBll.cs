using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace BLL.Base;

public abstract class BaseBll<TDal> : IBLL
where TDal : IUnitOfWork
{
    public abstract Task<int> SaveChangesAsync();
    public abstract int SaveChanges();
}