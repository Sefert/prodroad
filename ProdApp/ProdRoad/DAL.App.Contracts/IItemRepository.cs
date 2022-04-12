using Base.Contracts.DAL;
using Domain.App;

namespace DAL.App.Contracts;

public interface IItemRepository : IEntityRepository<Item>
{
    Task<IEnumerable<Item>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<Item?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}