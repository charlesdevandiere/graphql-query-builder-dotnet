using System;
using System.Reflection;
using Newtonsoft.Json;

namespace GraphQL.Query.Builder.Formatter.NewtonsoftJson;

/// <summary>The json property name formatter class.</summary>
public static class NewtonsoftJsonPropertyNameFormatter
{
    /// <summary>Return the JsonPropertyAttribute value if exist.</summary>
    /// <value>The property.</value>
    public static Func<PropertyInfo, string> Format = property =>
    {
        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        JsonPropertyAttribute attribute = property.GetCustomAttribute<JsonPropertyAttribute>();
        if (!string.IsNullOrEmpty(attribute?.PropertyName))
        {
            return attribute.PropertyName;
        }

        return property.Name;
    };
}
