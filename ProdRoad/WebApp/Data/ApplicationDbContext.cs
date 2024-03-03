using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid>
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
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}
}