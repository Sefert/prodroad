using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ApplicationUserRepo : BaseRepository<ApplicationUser>
    {
        public ApplicationUserRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}