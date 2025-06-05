using Api.Extensions.GraphQL.Attributes;
using Application.Repositories;
using Domain.Entities;

namespace Api.Extensions.GraphQL;

// [Authorize(Roles = ...)]
[ExtendObjectType(typeof(Query))]
public sealed class ConsultantQueryExtensions
{
    // [Authorize(Roles = ...)]
    [UseGraphQLDataPaging]
    public IQueryable<Consultant> SearchConsultants
    (
        [Service] IConsultantRepository repository
    )
    {
        return repository.GetAll();
    }
}
