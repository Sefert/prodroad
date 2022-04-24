using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceGroupRepository : BaseEntityRepository<DTO.App.PriceGroup, Domain.App.PriceGroup, AppDbContext>, IPriceGroupRepository
{
    public PriceGroupRepository(AppDbContext dbContext, IMapper<DTO.App.PriceGroup, Domain.App.PriceGroup> mapper) : base(dbContext, mapper)
    {
    }
}