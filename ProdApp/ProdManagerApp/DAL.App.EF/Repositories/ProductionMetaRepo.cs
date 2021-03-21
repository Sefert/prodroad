using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductionMetaRepo : BaseRepository<ProductionMeta> , IProductionMetaRepo
    {
        public ProductionMetaRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<ProductionMeta>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(p => p.Supply).ToListAsync();
        }

        public override async Task<ProductionMeta?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.Include(p => p.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}