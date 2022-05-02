using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcedureRepository : BaseEntityRepository<DAL.App.DTO.Procedure, Domain.App.Procedure, AppDbContext>, IProcedureRepository
{
    public ProcedureRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Procedure, Domain.App.Procedure> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.Procedure>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.Procedure?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}