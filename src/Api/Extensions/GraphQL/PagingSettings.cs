namespace Api.Extensions.GraphQL;

public sealed class PagingSettings
{
    public int DefaultPageSize { get; set; }
    public int MaxPageSize { get; set; }
    public bool IncludeTotalCount { get; set; }
}
