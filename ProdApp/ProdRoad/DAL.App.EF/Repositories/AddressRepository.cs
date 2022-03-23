using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;


namespace DAL.App.EF.Repositories;

public class AddressRepository  : BaseEntityRepository<Address, AppDbContext>, IAddressRepository
{
    public AddressRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}