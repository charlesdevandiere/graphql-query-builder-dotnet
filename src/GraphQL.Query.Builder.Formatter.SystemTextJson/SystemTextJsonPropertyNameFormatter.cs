using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace GraphQL.Query.Builder.Formatter.SystemTextJson;

/// <summary>The json property name formatter class.</summary>
public static class SystemTextJsonPropertyNameFormatter
{
    /// <summary>Return the JsonPropertyNameAttribute value if exist.</summary>
    /// <value>The property.</value>
    public static Func<PropertyInfo, string> Format = property =>
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        JsonPropertyNameAttribute attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (!string.IsNullOrEmpty(attribute?.Name))
        {
            return attribute.Name;
        }

        return property.Name;
    };
}
