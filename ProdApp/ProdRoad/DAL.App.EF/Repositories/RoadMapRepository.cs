using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class RoadMapRepository : BaseEntityRepository<RoadMap, AppDbContext>, IRoadMapRepository
{
    public RoadMapRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<RoadMap>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return await query.ToListAsync();
    }
    
    public async Task<RoadMap?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return await query.FirstOrDefaultAsync();
    }
}