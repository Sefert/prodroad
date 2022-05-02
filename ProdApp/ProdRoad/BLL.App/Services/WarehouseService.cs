using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class WarehouseService :
    BaseEntityService<Warehouse, DAL.App.DTO.Warehouse, IWarehouseRepository>, 
    IWarehouseService
{
    public WarehouseService(
        IWarehouseRepository repo, 
        IMapper<Warehouse, DAL.App.DTO.Warehouse> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Warehouse>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Warehouse?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}