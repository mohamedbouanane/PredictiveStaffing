namespace Api.Extensions.GraphQL;

public sealed class GraphQLSettings
{
    public PagingSettings PagingSettings { get; set; }
    public bool IncludeExceptionDetails { get; set; }
    public bool UseGraphQLUI { get; set; }
}
