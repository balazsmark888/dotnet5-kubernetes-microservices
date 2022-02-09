using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepareDatabase
    {
        public static void PopulateData(this IApplicationBuilder app, bool isProd)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<ApplicationDbContext>(), isProd);
            }
        }

        private static void SeedData(ApplicationDbContext context, bool isProd)
        {
            if (isProd)
            {
                context.Database.Migrate();
            }
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Yes"
                    },
                    new Platform
                    {
                        Name = "Dot Net Framework",
                        Publisher = "Microsoft",
                        Cost = "Yes"
                    },
                    new Platform
                    {
                        Name = "Dot Net Core",
                        Publisher = "Microsoft",
                        Cost = "Yes"
                    });
                context.SaveChanges();
            }
        }
    }
}
