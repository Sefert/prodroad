using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcessRepository : BaseEntityRepository<DTO.App.Process, Domain.App.Process, AppDbContext>, IProcessRepository
{
    public ProcessRepository(AppDbContext dbContext, IMapper<DTO.App.Process, Domain.App.Process> mapper) : base(dbContext, mapper)
    {
    }
}