using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WarehouseRepo : BaseRepository<Warehouse, AppDbContext>, IWarehouseRepo
    {
        public WarehouseRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}