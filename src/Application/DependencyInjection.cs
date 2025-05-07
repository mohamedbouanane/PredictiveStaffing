namespace Application;

using System.Reflection;
using Application.Mappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        _ = services.MapModelsToDtos();

        return services;
    }


    private static IServiceCollection MapModelsToDtos(this IServiceCollection services)
    {
        _ = services.AddAutoMapper
        (
            typeof(ConsultantMappingProfile).Assembly,
            typeof(MissionMappingProfile).Assembly
        );

        return services;
    }
}
