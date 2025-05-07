namespace Infrastructure.Seeders;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Infrastructure.Contexts;
using Domain.Entities;

public static class DatabaseSeeder
{
    public static void Seed(AppDbContext context)
    {
        var consId1 = Guid.NewGuid();
        var consId2 = Guid.NewGuid();
        if (!context.Consultants.Any())
        {
            List<Consultant> consultants =
            [
                new Consultant { Id = consId1, FirstName = "Jean", LastName = "Dupont", Skills = "C#, SQL", AvailabilityDate = null },
                new Consultant { Id = consId2, FirstName = "Marie", LastName = "Martin", Skills = "React, Node.js", AvailabilityDate = DateTime.Now.AddDays(10) }
            ];

            context.Consultants.AddRange(consultants);
        }

        if (!context.Missions.Any())
        {
            List<Mission> missions =
            [
                new Mission { Id = Guid.NewGuid(), ClientName = "Client A", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), ConsultantId = consId1 },
                new Mission { Id = Guid.NewGuid(), ClientName = "Client B", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6), ConsultantId = consId2 }
            ];

            context.Missions.AddRange(missions);
        }

        context.SaveChanges();
    }

    /// <summary> Applique les migrations (crée la DB test si elle n'existe pas)   </summary>
    /// <param name="app"></param>
    public static WebApplication UseDatabaseSeeding(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();

        Seed(dbContext);

        return app;
    }
}
