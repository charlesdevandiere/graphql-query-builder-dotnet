namespace GraphQL.Query.Builder;

/// <summary>
/// Represents a raw value that will be inserted as a GraphQL argument with no quotes
/// </summary>
/// <param name="value"></param>
public readonly struct RawString(string value)
{
    /// <summary>
    /// The raw value
    /// </summary>
    public string Value { get; } = value;
}
