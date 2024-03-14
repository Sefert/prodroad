using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

/*Overriding Guid to string conversion - no string setup*/
public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<CustomerPrice> CustomerPrices { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<ItemProcess> ItemProcesses { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Price> Prices { get; set; } = default!;
    public DbSet<OrderRow> OrderRows { get; set; } = default!;
    public DbSet<Process> Processes { get; set; } = default!;
    public DbSet<RoadMap> RoadMaps { get; set; } = default!;
    public DbSet<Team> Teams{ get; set; } = default!;
    public DbSet<UserTeam> UserTeams { get; set; } = default!;
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                Console.WriteLine(prop);
                prop.CurrentValue = ((DateTime) prop.CurrentValue).ToUniversalTime();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}