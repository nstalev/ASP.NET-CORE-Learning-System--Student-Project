using LearningSystem.Data;
using LearningSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const string adminEmail = "admin@abv.bg";
        private const string adminUsername = "admin";
        private const string adminName = "admin";
        private const string adminPassword = "admin123";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //To run Async methods in Non-Asyn method
                Task
                 .Run(async () =>
                 {
                     // Seed Roles
                     var roles = new[]
                                     {
                         WebConstants.AdministratorRole,
                         WebConstants.TrainerRole,
                         WebConstants.BlogAuthorRole
                     };

                     foreach (var role in roles)
                     {
                         var roleExists = await roleManager.RoleExistsAsync(role);

                         if (!roleExists)
                         {
                             await roleManager.CreateAsync(new IdentityRole
                             {
                                 Name = role
                             });
                         }
                     }

                     // Seed Admin User
                     var adminUser = await userManager.FindByEmailAsync(adminEmail);

                     if (adminUser == null)
                     {
                         // Create Admin User
                         adminUser = new User
                         {
                             UserName = adminUsername,
                             Email = adminEmail,
                             Name = adminName,
                             BirthDate = DateTime.UtcNow
                         };

                         var result = await userManager.CreateAsync(adminUser, adminPassword);

                         // Add User to Role
                         if (result.Succeeded)
                         {
                             await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                         }
                     }
                  
                 })
                 .GetAwaiter()
                 .GetResult();
            }

            return app;
        }
    }
}
