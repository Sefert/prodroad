using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcessRepository : BaseEntityRepository<DAL.App.DTO.Process, Domain.App.Process, AppDbContext>, IProcessRepository
{
    public ProcessRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Process, Domain.App.Process> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.Process>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.Process?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}