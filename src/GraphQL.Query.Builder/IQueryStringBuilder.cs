namespace GraphQL.Query.Builder;

/// <summary>The GraphQL query builder interface.</summary>
public interface IQueryStringBuilder
{
    /// <summary>Clears the string builder.</summary>
    void Clear();

    /// <summary>Builds the query.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    string Build<TSource>(IGraphQLField<TSource> query);

    /// <summary>Builds the query selection set, without the enclosing braces.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The GraphQL selection set as string.</returns>
    string BuildSelectionSet<TSource>(IGraphQLField<TSource> query);
}
