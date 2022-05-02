using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class ItemProcedureService :
    BaseEntityService<ItemProcedure, DAL.App.DTO.ItemProcedure, IItemProcedureRepository>, 
    IItemProcedureService
{
    public ItemProcedureService(
        IItemProcedureRepository repo, 
        IMapper<ItemProcedure, DAL.App.DTO.ItemProcedure> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<ItemProcedure>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<ItemProcedure?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}