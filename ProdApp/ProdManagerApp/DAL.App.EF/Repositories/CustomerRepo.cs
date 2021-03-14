using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CustomerRepo : BaseRepository<Customer>, ICustomerRepo
    {
        public CustomerRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}