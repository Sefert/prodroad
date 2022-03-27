using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class AppDataHelper
{
    public static async Task SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration conf)
    {
        //scoped version of DI
        //using cleans up after (calls context dispose)
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
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("user or role manager can not be null!");
            }
            var roles = new string[]
            {
                "admin",
                "manager",
                "user",
            };

            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo).Result;
                if (role == null)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole() { Name = roleInfo }).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Role creation fialed");
                    }
                }
            }

            var users = new (string username,string Password, string roles)[]
            {
                ("admin@admin.com","1.TestWebApp", "admin,manager,user"),
                ("manager@manager.com","1.TestWebApp","manager,user"),
                ("user@user.com","1.TestWebApp","user"),
                ("newuser@newuser.com","1.TestWebApp","")
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        UserName = userInfo.username,
                        Email = userInfo.username,
                        LockoutEnabled = false,
                        PhoneNumber = "1234567890",
                        NormalizedUserName = userInfo.username.ToUpper(),
                        NormalizedEmail = userInfo.username.ToUpper(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var identityResult = userManager.CreateAsync(user, userInfo.Password).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user, 
                        userInfo.roles.Split(',').Select(a => a.Trim())).Result;
                }
            }
        }
        if (conf.GetValue<bool>("DataInitialization:SeedData"))
        {
            //await SeedAddresses(context);
            //await SeedCustomers(context);
            //await SeedItems(context);
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
            Id = Guid.Parse("9504aaa1-ab82-4af2-9ba5-f0ffb77ae23e"),
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            Name =
            {
                ["et"] = "AS Kapsel",
                ["en"] = "AS Kapsel-en",
            },
            Registration = 
            {
                ["et"] = "12345678",
                ["en"] = "12345678-en",
            }
        };
        context.Customers.Add(data);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedItems(AppDbContext context)
    {
        var data = new Item()
        {
            Id = Guid.Parse("9504aab1-ab82-4af2-9ba5-f0ffb77ae23e"),
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
        //await context.SaveChangesAsync();
        
        data = new Item()
        {
            Id = Guid.Parse("9504aab2-ab82-4af2-9ba5-f0ffb77ae23e"),
            AppUserId = Guid.Parse("9504bbb0-ab82-4af2-9ba5-f0ffb77ae23e"),
            ItemId = Guid.Parse("9504aab1-ab82-4af2-9ba5-f0ffb77ae23e"),
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
        await context.SaveChangesAsync();
    }

}
