using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcessRepository : BaseEntityRepository<DAL.App.DTO.Process, Domain.App.Process, AppDbContext>, IProcessRepository
{
    public ProcessRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Process, Domain.App.Process> mapper) : base(dbContext, mapper)
    {
    }
}