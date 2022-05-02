using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IPriceService : IEntityService<BLL.App.DTO.Price>,
    IPriceRepositoryCustom<BLL.App.DTO.Price>
{
    
}