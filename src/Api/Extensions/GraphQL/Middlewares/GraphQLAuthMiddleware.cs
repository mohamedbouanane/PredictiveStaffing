using Microsoft.AspNetCore.Http;

namespace Api.Extensions.GraphQL.Middlewares;

public class GraphQLAuthMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        //if (context.Request.Path.StartsWithSegments("/graphql") &&
        //    context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        //{
        //    if (!context.User.Identity?.IsAuthenticated ?? true)
        //    {
        //        context.Response.StatusCode = 401;
        //        await context.Response.WriteAsync("Unauthorized");
        //        return;
        //    }
        //}

        await next(context);
    }
}
