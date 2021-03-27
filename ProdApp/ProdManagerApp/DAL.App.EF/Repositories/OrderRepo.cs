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
    public class OrderRepo : BaseRepository<Order>, IOrderRepo
    {
        public OrderRepo(DbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Order>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(o => o.Customer)
                .Where(c => c.AppUserId.Equals(userId)).ToListAsync();
        }
        
        public override async Task<Order?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId.Equals(userId));;
        }
    }
}