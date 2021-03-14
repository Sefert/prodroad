using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class SupplyRepo : BaseRepository<Supply>, ISupplyRepo
    {
        public SupplyRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Supply>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse).ToListAsync();
        }
        
        public override async Task<Supply> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(s => s.Component)
                .Include(s => s.Item)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}