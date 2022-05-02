using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class ProcessService :
    BaseEntityService<Process, DAL.App.DTO.Process, IProcessRepository>, 
    IProcessService
{
    public ProcessService(
        IProcessRepository repo, 
        IMapper<Process, DAL.App.DTO.Process> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Process>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Process?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}