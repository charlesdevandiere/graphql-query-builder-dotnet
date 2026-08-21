using System.Reflection;

namespace GraphQL.Query.Builder;

/// <summary>Shared helper for property filtering and default value checks.</summary>
internal static class PropertyHelper
{
    /// <summary>Checks whether a property's value on the given object is the default for its type.</summary>
    internal static bool IsDefaultValue(PropertyInfo property, object @object)
    {
        object? value = property.GetValue(@object);

        if (value is null)
        {
            return true;
        }

        Type type = property.PropertyType;
        if (type.IsValueType)
        {
            object? defaultValue = Activator.CreateInstance(type);
            return value.Equals(defaultValue);
        }

        return false;
    }

    /// <summary>Filters and resolves properties from an object based on the ignore condition.</summary>
    /// <returns>An ordered enumerable of (name, value) pairs.</returns>
    internal static IEnumerable<KeyValuePair<string, object?>> GetPropertyValues(
        object @object,
        QueryIgnoreCondition ignoreCondition,
        Func<PropertyInfo, string>? formatter)
    {
        // Instance properties only: a static property is not part of the object's value, and reading one
        // that returns its own declaring type (DateTimeOffset.Now, for instance) recurses forever.
        // Indexers cannot be read without arguments, and write-only properties cannot be read at all.
        IEnumerable<PropertyInfo> properties = @object.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

        if (ignoreCondition == QueryIgnoreCondition.WhenWritingNull)
        {
            properties = properties.Where(p => p.GetValue(@object) is not null);
        }
        else if (ignoreCondition == QueryIgnoreCondition.WhenWritingDefault)
        {
            properties = properties.Where(p => !IsDefaultValue(p, @object));
        }

        return properties
            .Select(p => new KeyValuePair<string, object?>(
                formatter is not null ? formatter.Invoke(p) : p.Name,
                p.GetValue(@object)))
            .OrderBy(kv => kv.Key);
    }
}
