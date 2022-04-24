using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class RoadMapRepository : BaseEntityRepository<DTO.App.RoadMap, Domain.App.RoadMap, AppDbContext>, IRoadMapRepository
{
    public RoadMapRepository(AppDbContext dbContext, IMapper<DTO.App.RoadMap, Domain.App.RoadMap> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<DTO.App.RoadMap>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<DTO.App.RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}