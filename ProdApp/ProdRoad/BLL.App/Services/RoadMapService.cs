using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class RoadMapService :
    BaseEntityService<RoadMap, DAL.App.DTO.RoadMap, IRoadMapRepository>, 
    IRoadMapService
{
    public RoadMapService(
        IRoadMapRepository repo, 
        IMapper<RoadMap, DAL.App.DTO.RoadMap> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<RoadMap>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}