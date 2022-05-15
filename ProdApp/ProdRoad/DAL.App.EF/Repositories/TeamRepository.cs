using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;
using Domain.Base;
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
            .Where(m => m.AppUserId == userId)
            .Include(ut => ut.UserTeams);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async Task<DAL.App.DTO.Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId.Equals(userId))
            .Include(ut => ut.UserTeams);
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
    
    /*TODO https://stackoverflow.com/questions/70332565/jetbrains-rider-debug-mode-evaluator-exception */
    public async Task<DAL.App.DTO.Team?> PublicTeamAsync(LangStr code, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.Code.Equals(code) && m.IsPublic.Equals(true));
        
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}