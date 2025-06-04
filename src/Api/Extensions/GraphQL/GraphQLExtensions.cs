using Api.Extensions.GraphQL.Middlewares;
using GraphQL.Server.Ui.Voyager;
using HotChocolate.Types.Pagination;

using ServiceDefaults.Extensions;

namespace Api.Extensions.GraphQL;

public static class GraphQLExtensions
{
    public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var graphQLSettings = configuration.GetSection<GraphQLSettings>();
        var pagingSettings = graphQLSettings.PagingSettings;

        _ = services.AddScoped<IGenericResolver, GenericResolver>();
        _ = services
            .AddGraphQLServer("appschema")
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .AddQueryType<Query>()
            .AddTypeExtension<ConsultantQueryExtensions>()
            //.AddMutationType<Mutation>()
            //.SetPagingOptions(new PagingOptions
            //{
            //    DefaultPageSize = pagingSettings.DefaultPageSize,
            //    MaxPageSize = pagingSettings.MaxPageSize,
            //    IncludeTotalCount = pagingSettings.IncludeTotalCount
            //})
            //.ModifyRequestOptions(opt =>
            //{
            //    opt.IncludeExceptionDetails = graphQLSettings.IncludeExceptionDetails;
            //})
            ;

        return services;
    }

    public static IApplicationBuilder UseGraphQLConfiguration(this IApplicationBuilder app)
    {
        var graphQLSettings = app.GetSection<GraphQLSettings>();

        app.MapGraphQL
        (
            path: "/api/graphql",
            schemaName: "appschema"
        );

        if (graphQLSettings.UseGraphQLUI)
        {
            //app.UseGraphQLPlayground(); // UI Playground pour HotChocolate
            app.UseGraphQLVoyager
            (
                path: "/api/graphql-voyager",
                options: new VoyagerOptions { GraphQLEndPoint = "/api/graphql" }
            );
        }

        // app.UseMiddleware<GraphQLAuthMiddleware>();

        return app;
    }
}