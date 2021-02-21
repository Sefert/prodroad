using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
    }
}