using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class CustomerPriceGroupRepository : BaseEntityRepository<CustomerPriceGroup, AppDbContext>, ICustomerPriceGroupRepository
{
    public CustomerPriceGroupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}