using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class NotificationTypeRepo : BaseRepository<NotificationType, AppDbContext>, INotificationTypeRepo
    {
        public NotificationTypeRepo(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}