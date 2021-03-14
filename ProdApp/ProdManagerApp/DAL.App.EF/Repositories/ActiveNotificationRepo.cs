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
    public class ActiveNotificationRepo : BaseRepository<ActiveNotification>, IActiveNotificationRepo
    {
        public ActiveNotificationRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<ActiveNotification>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            query = query.Include(e => e.MasterNotification)
                .Include(e => e.Order)
                .Include(a => a.Supply);
            var res = await query.ToListAsync();
            
            return res;
        }
        
        public override async Task<ActiveNotification> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(a => a.MasterNotification)
                .Include(a => a.Order)
                .Include(a => a.Supply)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        
    }
}