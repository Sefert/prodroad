using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TeamRepo : BaseRepository<Team> , ITeamRepo
    {
        public TeamRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}