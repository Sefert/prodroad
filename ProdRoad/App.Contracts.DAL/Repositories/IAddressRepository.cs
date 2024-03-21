using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IAddressRepository : IEntityRepository<Address>
{
    Task<IEnumerable<Address?>> GetWithCustomers(Guid addressId, bool noTracking = true);
}