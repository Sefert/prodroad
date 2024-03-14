using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AddressRepository : BaseEntityRepository<Guid,AppDbContext,Address>
{
    public AddressRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}