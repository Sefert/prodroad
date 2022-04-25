using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;


namespace DAL.App.EF.Repositories;

public class CustomerPriceGroupRepository : BaseEntityRepository<
        DAL.App.DTO.CustomerPriceGroup, 
    Domain.App.CustomerPriceGroup, 
    AppDbContext>, 
    ICustomerPriceGroupRepository
{
    public CustomerPriceGroupRepository(AppDbContext dbContext, 
        IMapper<DAL.App.DTO.CustomerPriceGroup, Domain.App.CustomerPriceGroup> mapper) : base(dbContext, mapper)
    {
    }
}