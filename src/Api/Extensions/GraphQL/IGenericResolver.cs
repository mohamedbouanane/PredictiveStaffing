using Domain.Entities.Common;

namespace Api.Extensions.GraphQL;

public interface IGenericResolver
{
    IQueryable<T> Resolve<T>() where T : BaseEntity;
    IQueryable<T> ResolveWithAuth<T>(string operation = "Read") where T : BaseEntity;
}
