namespace Application;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
