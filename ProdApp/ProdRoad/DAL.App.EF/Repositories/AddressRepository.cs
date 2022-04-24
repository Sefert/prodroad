using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class AddressRepository  : BaseEntityRepository<DTO.App.Address, Domain.App.Address, AppDbContext>, IAddressRepository
{
    public AddressRepository(AppDbContext dbContext, IMapper<DTO.App.Address, Domain.App.Address> mapper) : base(dbContext, mapper)
    {
        
    }
    
    public async Task<IEnumerable<DTO.App.Address>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<DTO.App.Address?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}