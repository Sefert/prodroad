using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class CustomerPriceGroupService :
    BaseEntityService<CustomerPriceGroup, DAL.App.DTO.CustomerPriceGroup, ICustomerPriceGroupRepository>, 
    ICustomerPriceGroupService
{
    public CustomerPriceGroupService(
        ICustomerPriceGroupRepository repo, 
        IMapper<CustomerPriceGroup, DAL.App.DTO.CustomerPriceGroup> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<CustomerPriceGroup>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<CustomerPriceGroup?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}