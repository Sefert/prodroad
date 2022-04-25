using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class CustomerPriceRepository : BaseEntityRepository<DAL.App.DTO.CustomerPrice,Domain.App.CustomerPrice, AppDbContext>, ICustomerPriceRepository
{
    public CustomerPriceRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.CustomerPrice,Domain.App.CustomerPrice> mapper) : base(dbContext, mapper)
    {
    }
}