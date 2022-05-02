using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IAddressService : IEntityService<BLL.App.DTO.Address>, 
    IAddressRepositoryCustom<BLL.App.DTO.Address>
{

}