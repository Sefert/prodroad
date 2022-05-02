using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class PriceService :
    BaseEntityService<Price, DAL.App.DTO.Price, IPriceRepository>, 
    IPriceService
{
    public PriceService(
        IPriceRepository repo, 
        IMapper<Price, DAL.App.DTO.Price> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Price>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Price?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}