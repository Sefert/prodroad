using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<ActiveNotification> ActiveNotifications { get; set; } = default!;
        public DbSet<Component> Components { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<ItemComponent> ItemComponents { get; set; } = default!;
        public DbSet<NotificationType> NotificationTypes { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderData> OrderDatas { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<Production> Productions { get; set; } = default!;
        public DbSet<ProductionMeta> ProductionMetas { get; set; } = default!;
        public DbSet<Supply> Supplys { get; set; } = default!;
        public DbSet<UserNotification> UserNotifications { get; set; } = default!;
    }
}