using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<Guid,AppDbContext,Customer>
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}