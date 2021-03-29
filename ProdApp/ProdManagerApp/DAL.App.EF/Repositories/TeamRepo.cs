using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TeamRepo : BaseRepository<Team, AppDbContext> , ITeamRepo
    {
        public TeamRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}