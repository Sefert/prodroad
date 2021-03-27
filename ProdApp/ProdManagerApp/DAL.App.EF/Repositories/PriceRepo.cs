using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepo : BaseRepository<Price>, IPriceRepo
    {
        public PriceRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Price>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(p => p.Component).
                Include(p => p.Item).ToListAsync();
        }

        public override async Task<Price?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}