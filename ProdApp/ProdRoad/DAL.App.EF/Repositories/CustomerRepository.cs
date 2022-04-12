using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<Customer, AppDbContext>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<Customer>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return await query.ToListAsync();
    }
    
    public async Task<Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return await query.FirstOrDefaultAsync();
    }
}