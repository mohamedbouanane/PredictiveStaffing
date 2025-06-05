namespace Infrastructure;

using Application.Repositories;
using Application.Repositories.Common;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Common;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, InfrastructureSettings settings)
    {
        _ = services.AddDbContext<AppDbContext>(options =>
        {
            ArgumentNullException.ThrowIfNull(settings);
            ArgumentNullException.ThrowIfNull(settings.DataBase1);
            ArgumentNullException.ThrowIfNull(settings.DataBase1.ConnectionStrings);
            ArgumentNullException.ThrowIfNull(settings.DataBase1.ConnectionStrings.PrimaryConnectionString);

            options
                .UseSqlite(connectionString: settings.DataBase1.ConnectionStrings.PrimaryConnectionString);

#if DEBUG
            options
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
#endif
        });

        _ = services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
        _ = services.AddScoped<IConsultantRepository, ConsultantRepository>();
        _ = services.AddScoped<IMissionRepository, MissionRepository>();

        return services;
    }
}

