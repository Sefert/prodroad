using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories;

public class CustomerPriceGroupRepository : 
    BaseEntityRepository<DAL.App.DTO.CustomerPriceGroup, Domain.App.CustomerPriceGroup, AppDbContext>, 
    ICustomerPriceGroupRepository
{
    public CustomerPriceGroupRepository(AppDbContext dbContext, 
        IMapper<DAL.App.DTO.CustomerPriceGroup, Domain.App.CustomerPriceGroup> mapper) : base(dbContext, mapper)
    {
    }

    //TODO: implement
    public Task<IEnumerable<DAL.App.DTO.CustomerPriceGroup>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.CustomerPriceGroup?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}