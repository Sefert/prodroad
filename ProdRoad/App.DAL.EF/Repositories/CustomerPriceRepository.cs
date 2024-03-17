using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CustomerPriceRepository : BaseEntityRepository<AppDbContext,CustomerPrice,CustomerPrice>, 
    ICustomerPriceRepository
{
    public CustomerPriceRepository(AppDbContext dbContext, IDalMapper<CustomerPrice, CustomerPrice> dalMapper) : 
        base(dbContext, dalMapper)
    {
    }
}