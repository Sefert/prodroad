using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}