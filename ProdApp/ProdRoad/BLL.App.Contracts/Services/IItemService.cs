using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IItemService : IEntityService<BLL.App.DTO.Item>, 
    IItemRepositoryCustom<BLL.App.DTO.Item>
{
    
}