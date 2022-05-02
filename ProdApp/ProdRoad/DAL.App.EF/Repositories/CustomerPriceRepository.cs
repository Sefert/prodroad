using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class CustomerPriceRepository : BaseEntityRepository<DAL.App.DTO.CustomerPrice,Domain.App.CustomerPrice, AppDbContext>, ICustomerPriceRepository
{
    public CustomerPriceRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.CustomerPrice,Domain.App.CustomerPrice> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.CustomerPrice>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.CustomerPrice?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}