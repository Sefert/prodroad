using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ItemRepo : BaseRepository<Item> , IItemRepo
    {
        public ItemRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}