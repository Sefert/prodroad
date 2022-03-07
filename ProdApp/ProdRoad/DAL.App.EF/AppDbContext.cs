using Domain.App;
using Domain.App.Identity;
using Domain.Base.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<ActiveNotification> ActiveNotifications { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<CustomerPriceGroup> CustomerPriceGroups { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<ItemProcedure> ItemProcedures { get; set; } = default!;
    public DbSet<ItemWarehouse> ItemWarehouses { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    public DbSet<PriceGroup> PriceGroups { get; set; } = default!;
    public DbSet<Procedure> Procedures { get; set; } = default!;
    public DbSet<Process> Processes { get; set; } = default!;
    public DbSet<RoadMap> RoadMaps { get; set; } = default!;
    public DbSet<Team> Teams { get; set; } = default!;
    public DbSet<UserNotification> UserNotifications { get; set; } = default!;
    public DbSet<UserTeam> UserTeams { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;
    public DbSet<CustomerPrice> CustomerPrices { get; set; } = default!;


    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        // Remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        FixEntities(this);
        
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }


    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);
        

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }
}
