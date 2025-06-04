using Application.Repositories;
using Domain.Entities;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions.GraphQL;

[ExtendObjectType(typeof(Query))]
public class ConsultantQueryExtensions
{
    //[UseDbContext(typeof(TaDbContext))]
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    [HotChocolate.Data.UseSorting]
    public IQueryable<Consultant> SearchConsultants
    (
        [Service] IConsultantRepository repository
    )
    {
        return repository.GetAll();
    }
}
