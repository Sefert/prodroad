using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<DTO.App.Customer,Domain.App.Customer, AppDbContext>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext, IMapper<DTO.App.Customer,Domain.App.Customer> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<DTO.App.Customer>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<DTO.App.Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query.Where(m => m.AppUserId.Equals(userId) && m.Id.Equals(id))
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);
        return Mapper.Map(await query.FirstOrDefaultAsync());
    }
}