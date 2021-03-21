using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductionRepo : BaseRepository<Production>, IProductionRepo
    {
        public ProductionRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Production>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta).ToListAsync();
        }
        
        public override async Task<Production?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}