using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IProcessService : IEntityService<BLL.App.DTO.Process>,
    IProcessRepositoryCustom<BLL.App.DTO.Process>
{
    
}