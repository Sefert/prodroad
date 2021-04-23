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
    public class ProductionRepo : BaseRepository<Production, AppDbContext>, IProductionRepo
    {
        public ProductionRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<Production>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta)
                .Where(p => p.ProductionMeta.AppUserId.Equals(userId)).ToListAsync();
        }
        
        public override async Task<Production?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(p => p.Component)
                .Include(p => p.Item)
                .Include(p => p.ProductionMeta)
                .FirstOrDefaultAsync(p => p.Id == id && p.ProductionMeta.AppUserId.Equals(userId));
        }
    }
}