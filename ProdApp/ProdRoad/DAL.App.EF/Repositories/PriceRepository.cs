using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceRepository : BaseEntityRepository<DTO.App.Price, Domain.App.Price, AppDbContext>, IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IMapper<DTO.App.Price, Domain.App.Price> mapper) : base(dbContext, mapper)
    {
    }
}