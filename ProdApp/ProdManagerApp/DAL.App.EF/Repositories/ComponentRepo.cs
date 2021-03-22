using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ComponentRepo : BaseRepository<Component>, IComponentRepo
    {
        public ComponentRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}