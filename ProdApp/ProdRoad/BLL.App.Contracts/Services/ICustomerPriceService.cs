using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface ICustomerPriceService : IEntityService<BLL.App.DTO.CustomerPrice>,
    ICustomerPriceRepositoryCustom<BLL.App.DTO.CustomerPrice>
{
    
}