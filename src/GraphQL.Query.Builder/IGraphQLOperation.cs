namespace GraphQL.Query.Builder;

/// <summary>The GraphQL operation interface.</summary>
public interface IGraphQLOperation
{
    /// <summary>Gets the operation type (query, mutation, subscription), or null for shorthand.</summary>
    OperationType? Type { get; }

    /// <summary>Gets the optional operation name.</summary>
    string? Name { get; }

    /// <summary>Builds the complete GraphQL operation string.</summary>
    /// <returns>The GraphQL operation as a string.</returns>
    string Build();
}
