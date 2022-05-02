using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IItemWarehouseService : IEntityService<BLL.App.DTO.ItemWarehouse>,
    IItemWarehouseRepositoryCustom<BLL.App.DTO.ItemWarehouse>
{
    
}