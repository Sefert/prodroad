using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class CustomerPriceRepository : BaseEntityRepository<CustomerPrice, AppDbContext>, ICustomerPriceRepository
{
    public CustomerPriceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}