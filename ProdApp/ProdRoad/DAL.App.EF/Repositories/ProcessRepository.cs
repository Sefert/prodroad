using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ProcessRepository : BaseEntityRepository<Process,AppDbContext>, IProcessRepository
{
    public ProcessRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}