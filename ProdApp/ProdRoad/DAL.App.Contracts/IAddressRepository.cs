using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface IAddressRepository : IEntityRepository<Address>
{
    //custom methods here (search, so on)
}