using System.Threading.Tasks;
using Contracts.DAL.BAse;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork : IBaseUnitOfWork
    {
        public abstract Task<int> SaveChangesAsync();
    }
}