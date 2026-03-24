using System.Reflection;

namespace GraphQL.Query.Builder;

/// <summary>The query options class.</summary>
public class QueryOptions
{
    /// <summary>Gets or sets the property name formatter.</summary>
    public Func<PropertyInfo, string>? Formatter { get; set; }

    /// <summary>Gets or sets the query string builder factory.</summary>
    public Func<IQueryStringBuilder>? QueryStringBuilderFactory { get; set; }

    /// <summary>
    /// Gets or sets the condition under which properties with null or default values are ignored during serialization.
    /// Defaults to <see cref="QueryIgnoreCondition.Never"/>.
    /// </summary>
    public QueryIgnoreCondition DefaultIgnoreCondition { get; set; } = QueryIgnoreCondition.Never;
}
