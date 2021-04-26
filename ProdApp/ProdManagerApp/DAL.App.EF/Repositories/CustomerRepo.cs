using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CustomerRepo : BaseRepository<Customer, AppDbContext>, ICustomerRepo
    {
        public CustomerRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}