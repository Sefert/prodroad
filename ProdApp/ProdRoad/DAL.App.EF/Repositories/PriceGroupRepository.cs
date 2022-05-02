using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceGroupRepository : BaseEntityRepository<DAL.App.DTO.PriceGroup, Domain.App.PriceGroup, AppDbContext>, IPriceGroupRepository
{
    public PriceGroupRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.PriceGroup, Domain.App.PriceGroup> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.PriceGroup>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.PriceGroup?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}