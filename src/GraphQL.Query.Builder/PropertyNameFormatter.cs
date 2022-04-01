using System;
using System.Reflection;
using Dawn;
using Newtonsoft.Json;

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

            Attribute jsonAttribute = property.GetCustomAttribute(typeof(JsonPropertyAttribute));

            if (jsonAttribute != null)
            {
                if (!string.IsNullOrEmpty((jsonAttribute as JsonPropertyAttribute).PropertyName))
                {
                    return (jsonAttribute as JsonPropertyAttribute).PropertyName;
                }
            }

            if (formatter != null)
            {
                return formatter.Invoke(property.Name);
            }

            return property.Name;
        }
    }
}
