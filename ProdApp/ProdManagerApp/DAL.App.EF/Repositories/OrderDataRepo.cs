using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderDataRepo : BaseRepository<OrderData, AppDbContext>, IOrderDataRepo
    {
        public OrderDataRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<OrderData>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(o => o.Component)
                .Include(o => o.Item)
                .Include(o => o.Order)
                .Include(o => o.Supply).ToListAsync();
        }
        
        public override async Task<OrderData?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(o => o.Component)
                .Include(o => o.Item)
                .Include(o => o.Order)
                .Include(o => o.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}