using System;
using System.Reflection;
using Dawn;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphQL.Query.Builder
{
    /// <summary>The query formatter class.</summary>
    internal static class PropertyNameFormatter
    {
        /// <summary>Tries to get property name from JSON property attribute or from optional formater.</summary>
        /// <param name="property">The property.</param>
        /// <param name="formatter">The formatter.</param>
        /// <returns>The property name.</returns>
        internal static string GetPropertyName(PropertyInfo property, Func<string, string> formatter = null)
        {
            Guard.Argument(property, nameof(property)).NotNull();

            JsonPropertyNameAttribute attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

            if (attribute is not null && !string.IsNullOrEmpty(attribute.Name))
            {
                return attribute.Name;
            }

            if (formatter != null)
            {
                return formatter.Invoke(property.Name);
            }

            return property.Name;
        }
    }
}
