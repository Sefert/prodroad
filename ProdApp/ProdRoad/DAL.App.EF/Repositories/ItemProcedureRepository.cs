using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ItemProcedureRepository : BaseEntityRepository<DAL.App.DTO.ItemProcedure, Domain.App.ItemProcedure, AppDbContext>, IItemProcedureRepository
{
    public ItemProcedureRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.ItemProcedure, Domain.App.ItemProcedure> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.ItemProcedure>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.ItemProcedure?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}