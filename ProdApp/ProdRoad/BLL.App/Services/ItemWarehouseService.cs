using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class ItemWarehouseService :
    BaseEntityService<ItemWarehouse, DAL.App.DTO.ItemWarehouse, IItemWarehouseRepository>, 
    IItemWarehouseService
{
    public ItemWarehouseService(
        IItemWarehouseRepository repo, 
        IMapper<ItemWarehouse, DAL.App.DTO.ItemWarehouse> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<ItemWarehouse>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<ItemWarehouse?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}