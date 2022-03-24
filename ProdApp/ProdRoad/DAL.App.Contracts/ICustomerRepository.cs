using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    
}