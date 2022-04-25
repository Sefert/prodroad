using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceRepository : BaseEntityRepository<DAL.App.DTO.Price, Domain.App.Price, AppDbContext>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Price, Domain.App.Price> mapper) : base(dbContext, mapper)
    {
    }
}