using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Authorization;
using Api.Extensions.GraphQL.Settings;
using HotChocolate.Types.Pagination;
using ServiceDefaults.Extensions;
using GraphQL.Server.Ui.Voyager;
using HotChocolate.Diagnostics;
using System.Reflection;

namespace Api.Extensions.GraphQL;

public static class GraphQLExtensions
{
    /// <summary>
    /// Configures the GraphQL endpoints and tools (e.g., Banana Cake Pop, Voyager)
    /// based on application settings. Registers the main schema path and optional UI tools.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration"></param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var graphQLSettings = configuration.GetSection<GraphQLSettings>();

        //_ = services.AddScoped<IGenericResolver, GenericResolver>();
        _ = services
            .AddGraphQLServer(graphQLSettings.SchemaName)
            .ConfigureExecutor(graphQLSettings)
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            //.AddMutationType<Mutation>()
            .AddGraphQLQueryWithExtensions<Query>();
            
        return services;
    }

    public static IApplicationBuilder UseGraphQLConfiguration(this IApplicationBuilder app)
    {
        var graphQLSettings = app.GetSection<GraphQLSettings>();
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        _ = app.MapGraphQL
        (
            path: graphQLSettings.ApiPath,
            schemaName: graphQLSettings.SchemaName
        )//.RequireAuthorization();
        ;

        if (!env.IsProduction())
        {
            _ = app.UseBananaCakePopConfiguration(settings: graphQLSettings.BananaCakePopSettings);
            _ = app.UseGraphQLVoyagerConfiguration(settings: graphQLSettings.GraphQLVoyagerSettings);
        }

        // app.UseMiddleware<GraphQLAuthMiddleware>();

        return app;
    }

    private static IApplicationBuilder UseBananaCakePopConfiguration(this IApplicationBuilder app, BananaCakePopSettings settings)
    {
        if (settings.ActiveUi)
        {
            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapBananaCakePop(toolPath: settings.UiPath)
                    .WithDisplayName("Banana Cake Pop")
                    //.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
                    ;
            });
        }

        return app;
    }

    private static IApplicationBuilder UseGraphQLVoyagerConfiguration(this IApplicationBuilder app, GraphQLVoyagerSettings settings)
    {
        if (settings.ActiveUi)
        {
            _ = app.UseGraphQLVoyager
            (
                path: settings.UiPath,
                options: new VoyagerOptions
                {
                    GraphQLEndPoint = settings.ApiPath
                }
            )//.RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            ;
        }

        return app;
    }

    /// <summary>
    /// Registers the root GraphQL query type along with all its associated
    /// type extensions marked with [ExtendObjectType(typeof(TQuery))].
    /// This enables automatic discovery and registration of modular query parts.
    /// </summary>
    /// <typeparam name="TQuery">The root query class.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The request executor builder.</returns>
    private static IRequestExecutorBuilder AddGraphQLQueryWithExtensions<TQuery>(this IRequestExecutorBuilder builder)
       where TQuery : class
    {
        builder.AddQueryType<TQuery>();

        var queryAssembly = typeof(TQuery).Assembly;
        var extensions = queryAssembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetCustomAttributes<ExtendObjectTypeAttribute>().Any(attr => attr.ExtendsType == typeof(TQuery)))
            .ToList();

        foreach (var extension in extensions)
        {
            // Appelle dynamiquement .AddTypeExtension<T>()
            var method = typeof(SchemaRequestExecutorBuilderExtensions)
                .GetMethods()
                .First(m => m.Name == "AddTypeExtension" && m.IsGenericMethod);

            var genericMethod = method.MakeGenericMethod(extension);
            genericMethod.Invoke(null, [builder]);
        }

        return builder;
    }

    // TODO : passer les params par settings
    private static IRequestExecutorBuilder ConfigureExecutor(this IRequestExecutorBuilder builder, GraphQLSettings settings)
    {
        _ = builder.ModifyOptions(o =>
        {
            o.StrictValidation = settings.StrictValidation;
            o.RemoveUnreachableTypes = settings.RemoveUnreachableTypes;
            o.SortFieldsByName = settings.SortFieldsByName;
            o.EnableDefer = true;
            o.EnableStream = true;
            o.EnableTag = true;
            o.MaxAllowedNodeBatchSize = 1000;
            o.EnsureAllNodesCanBeResolved = false;
            o.StrictRuntimeTypeValidation = false;
        });

        _ = builder.ModifyRequestOptions(o =>
        {
            o.IncludeExceptionDetails = settings.IncludeExceptionDetails;
            o.ExecutionTimeout = TimeSpan.FromSeconds(settings.ExecutionTimeoutSeconds);
        });

        _ = builder.SetPagingOptions(new PagingOptions
        {
            DefaultPageSize = settings.PagingSettings.DefaultPageSize,
            MaxPageSize = settings.PagingSettings.MaxPageSize,
            IncludeTotalCount = settings.PagingSettings.IncludeTotalCount
        });

        _ = builder.AddInstrumentation(o =>
        {
            o.IncludeDocument = true;
            o.Scopes = ActivityScopes.All;
            o.RequestDetails = RequestDetails.All;
        });

        return builder;
    }
}