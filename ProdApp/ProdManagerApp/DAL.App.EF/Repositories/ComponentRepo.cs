using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ComponentRepo : BaseRepository<Component,AppDbContext>, IComponentRepo
    {
        public ComponentRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}