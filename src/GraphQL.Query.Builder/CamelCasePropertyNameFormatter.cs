using System;
using System.Reflection;

namespace GraphQL.Query.Builder;

/// <summary>The camel case property name formatter class.</summary>
public static class CamelCasePropertyNameFormatter
{
    /// <summary>Formats the property name in camel case.</summary>
    /// <value>The property.</value>
    public static Func<PropertyInfo, string> FormatPropertyName = property =>
    {
        RequiredArgument.NotNull(property, nameof(property));

        return char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
    };

    /// <summary>Formats the type name in camel case.</summary>
    /// <value>The property.</value>
    public static Func<Type, string> FormatTypeName = type =>
    {
        RequiredArgument.NotNull(type, nameof(type));
        return char.ToLowerInvariant(type.Name[0]) + type.Name.Substring(1);
        //return char.ToLowerInvariant(type.Name[0]) + type.Name.Substring(1);
    };
}
