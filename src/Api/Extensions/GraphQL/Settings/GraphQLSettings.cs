namespace Api.Extensions.GraphQL.Settings;

public sealed class GraphQLSettings
{
    public string SchemaName { get; set; }
    public string ApiPath { get; set; }

    public bool IncludeExceptionDetails { get; set; }
    public int ExecutionTimeoutSeconds { get; set; }

    public bool StrictValidation { get; set; }
    public bool SortFieldsByName { get; set; }
    public bool RemoveUnreachableTypes { get; set; }

    public PagingSettings PagingSettings { get; set; }
    public BananaCakePopSettings BananaCakePopSettings { get; set; }
    public GraphQLVoyagerSettings GraphQLVoyagerSettings { get; set; }
}
