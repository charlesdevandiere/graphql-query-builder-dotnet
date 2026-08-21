namespace GraphQL.Query.Builder;

/// <summary>The GraphQL field interface.</summary>
public interface IGraphQLField
{
    /// <summary>Gets the query name.</summary>
    string Name { get; }

    /// <summary>Gets the alias name.</summary>
    string? AliasName { get; }

    /// <summary>Gets the directives attached to the field.</summary>
    List<GraphQLDirective> Directives { get; }

    /// <summary>Builds the field selection set, without the enclosing braces.</summary>
    /// <returns>The GraphQL selection set as string.</returns>
    string BuildSelectionSet();

    /// <summary>Builds the query.</summary>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    /// <exception cref="ArgumentException">Must have a 'Name' specified in the Query</exception>
    /// <exception cref="ArgumentException">Must have a one or more 'Select' fields in the Query</exception>
    string Build();
}
