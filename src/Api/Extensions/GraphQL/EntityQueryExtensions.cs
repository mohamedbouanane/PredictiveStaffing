//using Application.Repositories.Common;
//using Domain.Entities.Common;
//using HotChocolate;
//using HotChocolate.Data;
//using HotChocolate.Types;
//using HotChocolate.Types.Pagination;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;

//namespace Api.Extensions.GraphQL;

//[ExtendObjectType(typeof(Query))]
//public class EntityQueryExtensions
//{
//    [UseServiceScope]
//    [UsePaging]
//    [UseProjection]
//    [HotChocolate.Data.UseFiltering]
//    [HotChocolate.Data.UseSorting]
//    public IQueryable<T> GetEntities<T>
//    (
//       [Service] IBaseAsyncRepository<T> repository,
//       [Service] IAuthorizationService authService,
//       [Service] IHttpContextAccessor httpContextAccessor
//    )
//       where T : BaseEntity
//    {
//        // Vérification des droits d'accès
//        //if (!authService.CheckAccess(httpContextAccessor.HttpContext, typeof(T).Name, "Read"))
//        //{
//        //    throw new UnauthorizedAccessException($"Unauthorized access to {typeof(T).Name}");
//        //}

//        return repository.GetAll();
//    }

//    //[UseFirstOrDefault]
//    //public async Task<T?> GetEntityById<T>
//    //(
//    //    [Service] IBaseAsyncRepository<T> repository,
//    //    [Service] IAuthorizationService authService,
//    //    [Service] IHttpContextAccessor httpContextAccessor,
//    //    Guid id
//    //)
//    //    where T : BaseEntity
//    //{
//    //    //if (!authService.CheckAccess(httpContextAccessor.HttpContext, typeof(T).Name, "Read"))
//    //    //{
//    //    //    throw new UnauthorizedAccessException($"Unauthorized access to {typeof(T).Name}");
//    //    //}

//    //    return await repository.GetByIdAsync(id);
//    //}
//}

//public record PagingArguments
//(
//    int? First = null,
//    string? After = null,
//    int? Last = null,
//    string? Before = null
//);

//public record GetEntitiesQuery<T>
//(
//    PagingArguments Paging,
//    string? Where = null,
//    string? OrderBy = null
//) : IRequest<Connection<T>> where T : BaseEntity;
