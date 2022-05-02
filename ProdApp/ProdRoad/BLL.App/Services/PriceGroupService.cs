using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class PriceGroupService :
    BaseEntityService<PriceGroup, DAL.App.DTO.PriceGroup, IPriceGroupRepository>, 
    IPriceGroupService
{
    public PriceGroupService(
        IPriceGroupRepository repo, 
        IMapper<PriceGroup, DAL.App.DTO.PriceGroup> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<PriceGroup>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<PriceGroup?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}