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
    public class SupplyRepo : BaseRepository<Supply, AppDbContext>, ISupplyRepo
    {
        public SupplyRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Supply>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .Where(s =>s.Warehouse!.AppUserId.Equals(userId)).ToListAsync();
        }
        
        public override async Task<Supply?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .Where(s =>s.Warehouse!.AppUserId.Equals(userId))
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}