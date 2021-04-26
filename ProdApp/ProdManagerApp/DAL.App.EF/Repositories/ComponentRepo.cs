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
    public class ComponentRepo : BaseRepository<Component,AppDbContext>, IComponentRepo
    {
        public ComponentRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Component>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query
                .Where(c => c.UserUnits!
                    .Single(u => u.AppUserId.Equals(userId)).ComponentId.Equals(c.Id))
                .ToListAsync();
        }
        
        public override async Task<Component?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query
                .Where(c => c.UserUnits!
                    .Single(u => u.AppUserId.Equals(userId)).ComponentId.Equals(c.Id))
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}