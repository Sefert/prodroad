using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepo : BaseRepository<Item, AppDbContext> , IItemRepo
    {
        public ItemRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}