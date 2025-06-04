//using Domain.Entities.Common;
//using HotChocolate.Types;

//namespace Api.Extensions.GraphQL;

//[ExtendObjectType(typeof(Query))]
//public class GenericEntityExtensions(IGenericResolver resolver)
//{
//    [UsePaging]
//    [HotChocolate.Data.UseFiltering]
//    [HotChocolate.Data.UseSorting]
//    public IQueryable<T> GetEntities<T>() where T : BaseEntity
//        => resolver.Resolve<T>();
//}
