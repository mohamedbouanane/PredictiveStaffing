namespace Api.Extensions.GraphQL.Attributes;

using HotChocolate.Types;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class UseGraphQLDataPagingAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure
    (
        HotChocolate.Types.Descriptors.IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        System.Reflection.MemberInfo member
    )
    {
        _ = descriptor
            .UsePaging()
            .UseProjection()
            .UseFiltering()
            .UseSorting();
    }
}
