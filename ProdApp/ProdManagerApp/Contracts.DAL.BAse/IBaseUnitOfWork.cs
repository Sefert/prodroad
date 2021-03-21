using System.Threading.Tasks;

namespace Contracts.DAL.BAse
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}