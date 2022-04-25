using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class PriceGroupRepository : BaseEntityRepository<DAL.App.DTO.PriceGroup, Domain.App.PriceGroup, AppDbContext>, IPriceGroupRepository
{
    public PriceGroupRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.PriceGroup, Domain.App.PriceGroup> mapper) : base(dbContext, mapper)
    {
    }
}