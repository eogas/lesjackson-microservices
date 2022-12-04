using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>() ??
            throw new PlatformServiceException();
        SeedData(context, isProduction);
    }

    private static void SeedData(AppDbContext context, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");

            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not run migrations: {e.Message}");
            }
        }

        if (context.Platforms.Any())
        {
            Console.WriteLine("--> We already have data");
            return;
        }

        Console.WriteLine("Seeding data...");

        context.Platforms.AddRange(
            new Platform()
            {
                Name = "dotnet",
                Publisher = "Microsoft",
                Cost = "Free"
            },
            new Platform()
            {
                Name = "SQL Server Express",
                Publisher = "Microsoft",
                Cost = "Free"
            },
            new Platform()
            {
                Name = "Kubernetes",
                Publisher = "Cloud Native Computing Foundation",
                Cost = "Free"
            }
        );
        context.SaveChanges();
    }
}