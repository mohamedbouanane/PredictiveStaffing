using Application.Repositories.Common;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;

namespace Api.Extensions.GraphQL;

public class GenericResolver(
    IServiceProvider serviceProvider,
    IAuthorizationService authService,
    IHttpContextAccessor httpContextAccessor) : IGenericResolver
{
    public IQueryable<T> Resolve<T>() where T : BaseEntity
    {
        var repository = serviceProvider.GetRequiredService<IBaseAsyncRepository<T>>();
        return repository.GetAll();
    }

    public IQueryable<T> ResolveWithAuth<T>(string operation = "Read") where T : BaseEntity
    {
        //if (!_authService.CheckAccess(_httpContextAccessor.HttpContext, typeof(T).Name, operation))
        //{
        //    throw new UnauthorizedAccessException($"Unauthorized {operation} operation on {typeof(T).Name}");
        //}
        return Resolve<T>();
    }
}
