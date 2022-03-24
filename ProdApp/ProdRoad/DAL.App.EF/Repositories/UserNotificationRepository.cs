using DAL.App.Contracts;
using DAL.Base.EF;
using Domain.App;

namespace DAL.App.EF.Repositories;

public class UserNotificationRepository : BaseEntityRepository<UserNotification, AppDbContext>, IUserNotificationRepository
{
    public UserNotificationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}