using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class CustomerPriceRepository : BaseEntityRepository<DTO.App.CustomerPrice,Domain.App.CustomerPrice, AppDbContext>, ICustomerPriceRepository
{
    public CustomerPriceRepository(AppDbContext dbContext, IMapper<DTO.App.CustomerPrice,Domain.App.CustomerPrice> mapper) : base(dbContext, mapper)
    {
    }
}