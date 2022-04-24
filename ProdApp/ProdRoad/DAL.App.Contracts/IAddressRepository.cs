using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IAddressRepository : IEntityRepository<DTO.App.Address>
{
    //custom methods here (search, so on)
    Task<IEnumerable<DTO.App.Address>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DTO.App.Address?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}