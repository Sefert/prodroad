using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<AppDbContext,Order,Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IDalMapper<Order, Order> dalMapper) : base(dbContext, dalMapper)
    {
    }
}