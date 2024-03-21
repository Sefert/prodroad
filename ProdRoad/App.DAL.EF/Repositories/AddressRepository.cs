using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AddressRepository : BaseEntityRepository<AppDbContext,Address,Address>, IAddressRepository
{
    public AddressRepository(AppDbContext dbContext) : base(dbContext, new DalMapper<Address, Address>())
    {
    }
    
    public virtual async Task<IEnumerable<Address?>> GetWithCustomers(Guid addressId, bool noTracking = true)
    {
       return await CreateQuery().Include("Address").Where(a => a.Id.Equals(addressId))
           .Include(a => a.Customer).ToListAsync();
    }
    
}