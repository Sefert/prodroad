using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IAddressRepository : IEntityRepository<DAL.App.DTO.Address>
{
    //custom methods here (search, so on)
    Task<IEnumerable<DAL.App.DTO.Address>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<DAL.App.DTO.Address?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}