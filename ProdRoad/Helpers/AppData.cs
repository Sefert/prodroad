using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Data;

public static class AppData
{
    public static async Task SetupAppData(IApplicationBuilder app, IConfiguration conf)
    
    {
        //scoped version of DI
        //using cleans up after (calls context dispose)
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        
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
                "smartUser",
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
                        throw new ApplicationException("Role creation failed");
                    }
                }
            }

            var users = new (string username,string Password, string roles)[]
            {
                ("admin@admin.com","1.TestWebApp", "admin,manager,smartUser,user"),
                ("manager@manager.com","1.TestWebApp","manager,smartUser,user"),
                ("smartUser@smartUser.com","1.TestWebApp","smartUser,user"),
                ("user@user.com","1.TestWebApp","user")
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
                    if (!identityResultRole.Succeeded)
                    {
                        throw new ApplicationException("UserRole creation failed!");
                    }
                }
            }
        }
    }
}

