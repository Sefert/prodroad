using DAL.App.EF;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class AppDataHelper
{
    public static async Task SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration conf)
    {
        using var serviceScope = app.
            ApplicationServices.
            GetRequiredService<IServiceScopeFactory>().
            CreateScope();

        using var context = serviceScope.
            ServiceProvider.
            GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("service-problem : No DB context!");
        }

        if (conf.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        
        if (conf.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        
        if (conf.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            await SeedAddresses(context);
            await SeedCustomers(context);
            //SeedItems(context);
        }
    }

    private static async Task SeedAddresses(AppDbContext context)
    {
        var data = new Address()
        {
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            Country =
            {
                ["et"] = "Eesti",
                ["en"] = "Estonia",
            }
        };
        context.Addresses.Add(data);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedCustomers(AppDbContext context)
    {
        var data = new Customer()
        {
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            Name =
            {
                ["et"] = "AS Kapsel",
                ["en"] = "AS Kapsel",
            },
            Registration = 
            {
                ["et"] = "12345678",
                ["en"] = "12345678",
            }
        };
        context.Customers.Add(data);
        await context.SaveChangesAsync();
    }
    
    private static void SeedItems(AppDbContext context)
    {
        var data = new Item()
        {
            Id = Guid.Parse("9504bbb1-ab82-4af2-9ba5-f0ffb77ae23e"),
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            Name =
            {
                ["et"] = "T-särk",
                ["en"] = "T-shirt",
            },
            Type = 
            {
                ["et"] = "M4345",
                ["en"] = "M4345",
            },
            Unit = 
            {
                ["et"] = "tk",
                ["en"] = "piece",
            },
            Quantity = Convert.ToDecimal(75)
        };
        
        context.Items.Add(data);
        context.SaveChangesAsync();
        
        data = new Item()
        {
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            ItemId = Guid.Parse("9504bbb1-ab82-4af2-9ba5-f0ffb77ae23e"),
            Name =
            {
                ["et"] = "Käis",
                ["en"] = "Sleeve",
            },
            Type = 
            {
                ["et"] = "M4345-1",
                ["en"] = "M4345-1",
            },
            Unit = 
            {
                ["et"] = "tk",
                ["en"] = "piece",
            },
            Quantity = Convert.ToDecimal(25)
        };
        
        context.Items.Add(data);
        context.SaveChangesAsync();
    }

}
