using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ProcedureRepository : BaseEntityRepository<Procedure, AppDbContext>, IProcedureRepository
{
    public ProcedureRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}