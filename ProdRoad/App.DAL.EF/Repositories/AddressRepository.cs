using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AddressRepository : BaseEntityRepository<AppDbContext,Address,Address>, IAddressRepository
{
    public AddressRepository(AppDbContext dbContext) : base(dbContext, new DalMapper<Address, Address>())
    {
    }
}