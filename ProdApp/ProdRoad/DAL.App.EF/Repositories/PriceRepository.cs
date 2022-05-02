using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceRepository : BaseEntityRepository<DAL.App.DTO.Price, Domain.App.Price, AppDbContext>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Price, Domain.App.Price> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.Price>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.Price?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}