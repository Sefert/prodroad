using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ItemProcedureRepository : BaseEntityRepository<DAL.App.DTO.ItemProcedure, Domain.App.ItemProcedure, AppDbContext>, IItemProcedureRepository
{
    public ItemProcedureRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.ItemProcedure, Domain.App.ItemProcedure> mapper) : base(dbContext, mapper)
    {
    }
}