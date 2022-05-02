using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class CustomerPriceService :
    BaseEntityService<CustomerPrice, DAL.App.DTO.CustomerPrice, ICustomerPriceRepository>, 
    ICustomerPriceService
{
    public CustomerPriceService(
        ICustomerPriceRepository repo, 
        IMapper<CustomerPrice, DAL.App.DTO.CustomerPrice> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<CustomerPrice>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<CustomerPrice?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}