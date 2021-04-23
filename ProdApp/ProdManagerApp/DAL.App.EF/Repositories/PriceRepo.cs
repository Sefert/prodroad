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
    public class PriceRepo : BaseRepository<Price, AppDbContext>, IPriceRepo
    {
        public PriceRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Price>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .Where(p => p.Item!.Supplys!
                    .Single(s => s.Warehouse.AppUserId.Equals(userId) && p.Item.Id.Equals(s.ItemId)).Id.Equals(p.ItemId))
                .ToListAsync();
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
                .Where(p => p.Item!.Supplys!
                    .Single(s => s.Warehouse.AppUserId.Equals(userId) && p.Item.Id.Equals(s.ItemId)).Id.Equals(p.ItemId))
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}