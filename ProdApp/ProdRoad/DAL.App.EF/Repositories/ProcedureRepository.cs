using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ProcedureRepository : BaseEntityRepository<DTO.App.Procedure, Domain.App.Procedure, AppDbContext>, IProcedureRepository
{
    public ProcedureRepository(AppDbContext dbContext, IMapper<DTO.App.Procedure, Domain.App.Procedure> mapper) : base(dbContext, mapper)
    {
    }
}