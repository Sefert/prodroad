using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<AppDbContext,Customer,Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext, new DalMapper<Customer, Customer>())
    {
    }
}