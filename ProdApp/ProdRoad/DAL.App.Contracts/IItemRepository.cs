using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IItemRepository : IEntityRepository<DTO.App.Item>
{
    Task<IEnumerable<DTO.App.Item>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DTO.App.Item?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}