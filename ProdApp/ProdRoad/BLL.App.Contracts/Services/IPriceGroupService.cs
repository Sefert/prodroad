using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IPriceGroupService : IEntityService<BLL.App.DTO.PriceGroup>,
    IPriceGroupRepositoryCustom<BLL.App.DTO.PriceGroup>
{
    
}