using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface ICustomerRepository : IEntityRepository<DAL.App.DTO.Customer>
{
    Task<IEnumerable<DAL.App.DTO.Customer>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DAL.App.DTO.Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}