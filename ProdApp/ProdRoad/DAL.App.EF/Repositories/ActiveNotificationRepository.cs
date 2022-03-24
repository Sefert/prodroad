using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class ActiveNotificationRepository : BaseEntityRepository<ActiveNotification, AppDbContext>, IActiveNotificationRepository
{
    public ActiveNotificationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}