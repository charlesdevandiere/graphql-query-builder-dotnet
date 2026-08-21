namespace GraphQL.Query.Builder;

/// <summary>The GraphQL operation type.</summary>
public enum OperationType
{
    /// <summary>A read-only fetch operation.</summary>
    Query,

    /// <summary>A write operation followed by a fetch.</summary>
    Mutation,

    /// <summary>A long-lived request that fetches data in response to source events.</summary>
    Subscription
}
