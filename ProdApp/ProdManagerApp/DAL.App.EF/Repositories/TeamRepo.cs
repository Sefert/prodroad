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
    public class TeamRepo : BaseRepository<Team, AppDbContext> , ITeamRepo
    {
        public TeamRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            if (noTracking) {query = query.AsNoTracking();}

            return await query.Where(t => t.UserTeams!.Single(u => 
                u.AppUserId.Equals(userId)).TeamId.Equals(t.Id)).ToListAsync();
        }
        
        public override async Task<Team?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.FirstOrDefaultAsync(m => 
                m.Id == id && m.UserTeams!.Single(u => 
                    u.TeamId.Equals(m.Id)).AppUserId.Equals(id));
        }
    }
}