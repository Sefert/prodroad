using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcedureRepository : BaseEntityRepository<DAL.App.DTO.Procedure, Domain.App.Procedure, AppDbContext>, IProcedureRepository
{
    public ProcedureRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Procedure, Domain.App.Procedure> mapper) : base(dbContext, mapper)
    {
    }
}