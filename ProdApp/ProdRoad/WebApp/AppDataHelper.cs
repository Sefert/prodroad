using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration conf)
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
        }
        
        //TODO: fix et-EE and so on
        if (conf.GetValue<bool>("DataInitialization:SeedData"))
        {
            var address = new Address()
            {
                AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
                Country =
                {
                    ["et"] = "Eesti",
                    ["en"] = "Estonia",
                }
            };
            context.Addresses.Add(address);
            context.SaveChanges();
        }
    }

}
