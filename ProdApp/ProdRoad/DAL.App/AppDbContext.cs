using Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App;

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
    public DbSet<Team> Teams{ get; set; } = default!;
    public DbSet<UserNotification> UserNotifications { get; set; } = default!;
    public DbSet<UserTeam> UserTeams { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}