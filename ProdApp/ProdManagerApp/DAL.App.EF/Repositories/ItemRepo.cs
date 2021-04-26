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
    public class ItemRepo : BaseRepository<Item, AppDbContext> , IItemRepo
    {
        public ItemRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Item>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query
                .Where(i => i.UserUnits!
                    .Single(u => u.AppUserId.Equals(userId)).ItemId.Equals(i.Id))
                .ToListAsync();
        }
        
        public override async Task<Item?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query
                .Where(i => i.UserUnits!
                    .Single(u => u.AppUserId.Equals(userId)).ItemId.Equals(i.Id))
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}