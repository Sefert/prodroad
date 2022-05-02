using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class ItemService :
    BaseEntityService<Item, DAL.App.DTO.Item, IItemRepository>, 
    IItemService
{
    public ItemService(
        IItemRepository repo, 
        IMapper<Item, DAL.App.DTO.Item> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Item>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Item?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}