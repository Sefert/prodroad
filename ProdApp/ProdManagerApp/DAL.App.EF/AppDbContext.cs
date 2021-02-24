using System.Linq;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ActiveNotification> ActiveNotifications { get; set; } = default!;
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;
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
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<UserNotification> UserNotifications { get; set; } = default!;
        public DbSet<UserTeam> UserTeams { get; set; } = default!;
        public DbSet<Warehouse> Warehouses { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

    }
}