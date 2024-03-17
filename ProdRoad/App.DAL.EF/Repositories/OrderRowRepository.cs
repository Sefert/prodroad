using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderRowRepository : BaseEntityRepository<AppDbContext,OrderRow,OrderRow>, IOrderRowRepository
{
    public OrderRowRepository(AppDbContext dbContext, IDalMapper<OrderRow, OrderRow> dalMapper) : base(dbContext, dalMapper)
    {
    }
}