using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
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
        
        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
        using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

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
            if (userManager == null)
            {
                throw new NullReferenceException("user or role manager can not be null!");
            }
            var user = userManager.FindByEmailAsync("manager@manager.com").Result;
            await SeedAddresses(user, context);
            await SeedCustomers(user, context);
            await SeedItems(user, context);
        }
    }

    private static async Task SeedAddresses(AppUser user, AppDbContext context)
    {
        var data = new Address()
        {
            AppUserId = user.Id,
            Country = new LangStr()
            {
                ["et"] = "Eesti",
                ["en"] = "Estonia",
            }
        };
        context.Addresses.Add(data);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedCustomers(AppUser user, AppDbContext context)
    {
        var data = new Customer()
        {
            AppUserId = user.Id,
            Name = new LangStr()
            {
                ["et"] = "AS Kapsel",
                ["en"] = "AS Kapsel-en",
            },
            Registration =  new LangStr()
            {
                ["et"] = "12345678",
                ["en"] = "12345678-en",
            }
        };
        context.Customers.Add(data);
        await context.SaveChangesAsync();
    }
    
    private static async Task SeedItems(AppUser user, AppDbContext context)
    {
        var data = new Item()
        {
            AppUserId = user.Id,
            Name = new LangStr()
            {
                ["et"] = "T-särk",
                ["en"] = "T-shirt",
            },
            Type = new LangStr()
            {
                ["et"] = "M4345",
                ["en"] = "M4345",
            },
            Unit = new LangStr()
            {
                ["et"] = "tk",
                ["en"] = "piece",
            },
            Quantity = Convert.ToDecimal(75)
        };
        
        context.Items.Add(data);
        //await context.SaveChangesAsync();
        /*await context.Entry(user)
            .Collection(u => u.Items!)
            .Query()
            .ToListAsync();*/
        
        data = new Item()
        {
            AppUserId = user.Id,
            ItemId = user.Items!.FirstOrDefault()!.ItemId,
            Name = new LangStr()
            {
                ["et"] = "Käis",
                ["en"] = "Sleeve",
            },
            Type = new LangStr()
            {
                ["et"] = "M4345-1",
                ["en"] = "M4345-1",
            },
            Unit = new LangStr()
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
