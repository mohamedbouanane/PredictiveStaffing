namespace Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Repositories;
using Application.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Settings;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, InfrastructureSettings settings)
    {
        _ = services.AddDbContextPool<AppDbContext>(options =>
        {
            options
                .UseSqlite(settings.DataBase1.ConnectionStrings);

#if DEBUG
            options
            .EnableThreadSafetyChecks(enableChecks: false);

            options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
#endif
        });

        _ = services.AddScoped<IConsultantRepository, ConsultantRepository>();
        _ = services.AddScoped<IMissionRepository, MissionRepository>();

        return services;
    }
}

