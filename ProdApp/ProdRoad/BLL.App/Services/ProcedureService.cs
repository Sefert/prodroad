using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class ProcedureService :
    BaseEntityService<Procedure, DAL.App.DTO.Procedure, IProcedureRepository>, 
    IProcedureService
{
    public ProcedureService(
        IProcedureRepository repo, 
        IMapper<Procedure, DAL.App.DTO.Procedure> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Procedure>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Procedure?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}