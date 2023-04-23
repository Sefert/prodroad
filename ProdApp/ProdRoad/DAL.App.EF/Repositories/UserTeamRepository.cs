using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.EF.Migrations;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<DAL.App.DTO.UserTeam, Domain.App.UserTeam, AppDbContext>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.UserTeam, Domain.App.UserTeam> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<DAL.App.DTO.UserTeam>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async Task<DAL.App.DTO.UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId.Equals(userId));
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
    
    public async Task<DAL.App.DTO.UserTeam?> FirstOrDefaultTeamIdAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId.Equals(userId));
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}