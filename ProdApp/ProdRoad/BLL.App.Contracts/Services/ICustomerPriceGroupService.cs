using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface ICustomerPriceGroupService : IEntityService<BLL.App.DTO.CustomerPriceGroup>,
    ICustomerPriceGroupRepositoryCustom<BLL.App.DTO.CustomerPriceGroup>
{
    
}