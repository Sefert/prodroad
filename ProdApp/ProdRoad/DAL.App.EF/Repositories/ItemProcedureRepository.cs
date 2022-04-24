using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class ItemProcedureRepository : BaseEntityRepository<DTO.App.ItemProcedure, Domain.App.ItemProcedure, AppDbContext>, IItemProcedureRepository
{
    public ItemProcedureRepository(AppDbContext dbContext, IMapper<DTO.App.ItemProcedure, Domain.App.ItemProcedure> mapper) : base(dbContext, mapper)
    {
    }
}