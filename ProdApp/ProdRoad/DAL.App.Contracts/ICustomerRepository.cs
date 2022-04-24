using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ICustomerRepository : IEntityRepository<DTO.App.Customer>
{
    Task<IEnumerable<DTO.App.Customer>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DTO.App.Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}