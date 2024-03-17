using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ProcessRepository : BaseEntityRepository<AppDbContext,Process,Process>, IProcessRepository
{
    public ProcessRepository(AppDbContext dbContext, IDalMapper<Process, Process> dalMapper) : base(dbContext, dalMapper)
    {
    }
}