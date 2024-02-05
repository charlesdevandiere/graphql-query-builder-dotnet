using System;
using System.Reflection;

namespace GraphQL.Query.Builder;

/// <summary>The camel case property name formatter class.</summary>
public static class CamelCasePropertyNameFormatter
{
    /// <summary>Formats the property name in camel case.</summary>
    /// <value>The property.</value>
    public static Func<PropertyInfo, string> Formatter = property =>
    {
        RequiredArgument.NotNull(property, nameof(property));

        return char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
    };
}
