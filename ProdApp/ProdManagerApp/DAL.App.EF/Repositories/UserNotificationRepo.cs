using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserNotificationRepo : BaseRepository<UserNotification>, IUserNotificationRepo
    {
        public UserNotificationRepo(DbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<UserNotification>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query.Include(u => u.NotificationType)
                .ToListAsync();
        }
        
        public override async Task<UserNotification?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query.Include(u => u.NotificationType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}