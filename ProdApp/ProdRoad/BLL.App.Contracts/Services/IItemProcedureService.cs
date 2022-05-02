using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IItemProcedureService : IEntityService<BLL.App.DTO.ItemProcedure>,
    IItemProcedureRepositoryCustom<BLL.App.DTO.ItemProcedure>

{
    
}