using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IWarehouseService : IEntityService<BLL.App.DTO.Warehouse>,
    IWarehouseRepositoryCustom<BLL.App.DTO.Warehouse>
{
    
}