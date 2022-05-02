using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IProcedureService: IEntityService<BLL.App.DTO.Procedure>,
    IProcedureRepositoryCustom<BLL.App.DTO.Procedure>
{
    
}