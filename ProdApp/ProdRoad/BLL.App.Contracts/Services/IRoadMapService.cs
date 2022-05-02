using Base.Contracts.BLL;
using DAL.App.Contracts;

namespace BLL.App.Contracts.Services;

public interface IRoadMapService : IEntityService<BLL.App.DTO.RoadMap>,
    IRoadMapRepositoryCustom<BLL.App.DTO.RoadMap>
{
    
}