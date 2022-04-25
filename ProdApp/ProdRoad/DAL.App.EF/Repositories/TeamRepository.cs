using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class TeamRepository : BaseEntityRepository<DAL.App.DTO.Team, Domain.App.Team, AppDbContext>, ITeamRepository
{
    public TeamRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.Team, Domain.App.Team> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<DAL.App.DTO.Team>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<DAL.App.DTO.Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }

}