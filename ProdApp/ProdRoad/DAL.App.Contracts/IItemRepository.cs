using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IItemRepository : IEntityRepository<DAL.App.DTO.Item>
{
    Task<IEnumerable<DAL.App.DTO.Item>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DAL.App.DTO.Item?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}