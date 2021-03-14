using System;
using System.Collections.Generic;
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
        
        public override async Task<IEnumerable<UserTeam>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(u => u.Team).ToListAsync();
        }
        
        public override async Task<UserTeam> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(u => u.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}