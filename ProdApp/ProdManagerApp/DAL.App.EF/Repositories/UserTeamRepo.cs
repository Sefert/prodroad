using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserTeamRepo : BaseRepository<UserTeam>, IUserTeamRepo
    {
        public UserTeamRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<UserTeam>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query
                .Include(u => u.Team)
                .Where(c => c.AppUserId.Equals(userId))
                .ToListAsync();
        }
        
        public override async Task<UserTeam?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId.Equals(userId));
        }
    }
}