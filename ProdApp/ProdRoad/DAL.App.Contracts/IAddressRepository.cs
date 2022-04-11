using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface IAddressRepository : IEntityRepository<Address>
{
    //custom methods here (search, so on)
    Task<IEnumerable<Address>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<Address?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}