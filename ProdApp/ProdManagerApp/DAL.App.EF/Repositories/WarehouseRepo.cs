using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WarehouseRepo : BaseRepository<Warehouse>, IWarehouseRepo
    {
        public WarehouseRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}