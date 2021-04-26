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
    public class UserUnitRepo : BaseRepository<UserUnit, AppDbContext>, IUserUnitRepo
    {
        public UserUnitRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public override async Task<IEnumerable<UserUnit>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}

            return await query
                /*.Include(u => u.AppUser)*/
                .Include(u => u.Component)
                .Include(u => u.Item)
                .Where(u => u.AppUserId.Equals(userId))
                .ToListAsync();
        }
        
        public override async Task<UserUnit?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking) {query = query.AsNoTracking();}
            
            return await query
                /*.Include(u => u.AppUser)*/
                .Include(u => u.Component)
                .Include(u => u.Item)
                .FirstOrDefaultAsync(u => u.Id == id && u.AppUserId.Equals(userId));
        }
    }
}