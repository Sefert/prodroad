using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemComponentRepo : BaseRepository<ItemComponent>, IItemComponentRepo
    {
        public ItemComponentRepo(DbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<ItemComponent>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(i => i.Component)
                    .Include(i => i.Item).ToListAsync();
        }
        
        public override async Task<ItemComponent?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(i => i.Component)
                .Include(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}