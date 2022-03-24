using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ItemProcedureRepository : BaseEntityRepository<ItemProcedure, AppDbContext>, IItemProcedureRepository
{
    public ItemProcedureRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}