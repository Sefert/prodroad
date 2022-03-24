using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<Customer, AppDbContext>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}