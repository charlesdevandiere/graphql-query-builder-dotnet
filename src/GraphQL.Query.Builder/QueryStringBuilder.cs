using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace GraphQL.Query.Builder;

/// <summary>The GraphQL query builder class.</summary>
public class QueryStringBuilder : IQueryStringBuilder
{
    /// <summary>The property name formatter.</summary>
    protected readonly Func<PropertyInfo, string>? formatter;

    /// <summary>The ignore condition for null/default properties.</summary>
    protected readonly QueryIgnoreCondition ignoreCondition;

    /// <summary>The query string builder.</summary>
    public StringBuilder QueryString { get; } = new();

    /// <summary>Initializes a new instance of the <see cref="QueryStringBuilder" /> class.</summary>
    public QueryStringBuilder() : this(null, QueryIgnoreCondition.Never) { }

    /// <summary>Initializes a new instance of the <see cref="QueryStringBuilder" /> class.</summary>
    /// <param name="formatter">The property name formatter</param>
    public QueryStringBuilder(Func<PropertyInfo, string> formatter) : this(formatter, QueryIgnoreCondition.Never) { }

    /// <summary>Initializes a new instance of the <see cref="QueryStringBuilder" /> class.</summary>
    /// <param name="formatter">The property name formatter</param>
    /// <param name="ignoreCondition">The ignore condition for null/default properties</param>
    public QueryStringBuilder(Func<PropertyInfo, string>? formatter, QueryIgnoreCondition ignoreCondition)
    {
        this.formatter = formatter;
        this.ignoreCondition = ignoreCondition;
    }

    /// <summary>Builds the query.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    public string Build<TSource>(IQuery<TSource> query)
    {
        if (!string.IsNullOrWhiteSpace(query.AliasName))
        {
            this.QueryString.Append($"{query.AliasName}:");
        }

        this.QueryString.Append(query.Name);

        if (query.Arguments.Count > 0)
        {
            this.QueryString.Append("(");
            this.AddParams(query);
            this.QueryString.Append(")");
        }

        if (query.SelectList.Count > 0)
        {
            this.QueryString.Append("{");
            this.AddFields(query);
            this.QueryString.Append("}");
        }

        return this.QueryString.ToString();
    }

    /// <summary>Clears the string builder.</summary>
    public void Clear()
    {
        this.QueryString.Clear();
    }

    /// <summary>
    /// Formats query param.
    /// 
    /// Returns:
    ///   <list type="bullet">
    ///     <item>
    ///       <term>null</term>
    ///       <description><c>null</c></description>
    ///     </item>
    ///     <item>
    ///       <term>String</term>
    ///       <description><c>"foo"</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Number</term>
    ///       <description><c>10</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Boolean</term>
    ///       <description><c>true</c> or <c>false</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Enum</term>
    ///       <description><c>EnumValue</c></description>
    ///     </item>
    ///     <item>
    ///       <term>DateTime</term>
    ///       <description><c>"2024-06-15T13:45:30.0000000Z"</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Key value pair</term>
    ///       <description><c>foo:"bar"</c> or <c>foo:10</c> ...</description>
    ///     </item>
    ///     <item>
    ///       <term>List</term>
    ///       <description><c>["foo","bar"]</c> or <c>[1,2]</c> ...</description>
    ///     </item>
    ///     <item>
    ///       <term>Dictionary</term>
    ///       <description><c>{foo:"bar",b:10}</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Object</term>
    ///       <description><c>{foo:"bar",b:10}</c></description>
    ///     </item>
    ///   </list>
    /// </summary>
    /// <param name="value"></param>
    /// <returns>The formatted query param.</returns>
    /// <exception cref="InvalidDataException">Invalid Object Type in Param List</exception>
    protected internal virtual string FormatQueryParam(object? value) =>
        this.FormatQueryParam(value, new HashSet<object>(ReferenceComparer.Instance));

    /// <summary>Reference equality comparer for circular reference detection.</summary>
    private sealed class ReferenceComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceComparer Instance = new();
        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }

    /// <summary>Formats query param with circular reference detection.</summary>
    private string FormatQueryParam(object? value, HashSet<object> visited)
    {
        switch (value)
        {
            case null:
                return "null";

            case string strValue:
                string encoded = strValue
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\t", "\\t");
                return $"\"{encoded}\"";

            case char charValue:
                return $"\"{charValue}\"";

            case byte byteValue:
                return byteValue.ToString();

            case sbyte sbyteValue:
                return sbyteValue.ToString();

            case short shortValue:
                return shortValue.ToString();

            case ushort ushortValue:
                return ushortValue.ToString();

            case int intValue:
                return intValue.ToString();

            case uint uintValue:
                return uintValue.ToString();

            case long longValue:
                return longValue.ToString();

            case ulong ulongValue:
                return ulongValue.ToString();

            case float floatValue:
                return floatValue.ToString(CultureInfo.InvariantCulture);

            case double doubleValue:
                return doubleValue.ToString(CultureInfo.InvariantCulture);

            case decimal decimalValue:
                return decimalValue.ToString(CultureInfo.InvariantCulture);

            case bool booleanValue:
                return booleanValue ? "true" : "false";

            case Enum enumValue:
                return enumValue.ToString();

            case DateTime dateTimeValue:
                return this.FormatQueryParam(dateTimeValue.ToString("o"), visited);

            case { } kvValue when IsStringKeyValuePair(kvValue, out string? kvKey, out object? kvVal):
                return $"{kvKey}:{this.FormatQueryParam(kvVal, visited)}";

            case IDictionary<string, object?> dictValue:
                return $"{{{string.Join(",", dictValue.Select(e => $"{e.Key}:{this.FormatQueryParam(e.Value, visited)}"))}}}";

            case IEnumerable enumerableValue:
                List<string> items = [];
                foreach (object item in enumerableValue)
                {
                    items.Add(this.FormatQueryParam(item, visited));
                }
                return $"[{string.Join(",", items)}]";

            case { } objectValue:
                if (!visited.Add(objectValue))
                {
                    throw new InvalidOperationException("Circular reference detected.");
                }

                try
                {
                    Dictionary<string, object?> dictionary = this.ObjectToDictionary(objectValue);
                    return this.FormatQueryParam(dictionary, visited);
                }
                finally
                {
                    visited.Remove(objectValue);
                }

            default:
                throw new InvalidDataException($"Invalid Object Type in Param List: {value.GetType()}");
        }
    }

    /// <summary>Adds query params to the query string.</summary>
    /// <param name="query">The query.</param>
    protected internal void AddParams<TSource>(IQuery<TSource> query)
    {
        RequiredArgument.NotNull(query, nameof(query));

        foreach (KeyValuePair<string, object?> param in query.Arguments)
        {
            this.QueryString.Append($"{param.Key}:{this.FormatQueryParam(param.Value)},");
        }

        if (query.Arguments.Count > 0)
        {
            this.QueryString.Length--;
        }
    }

    /// <summary>Adds fields to the query sting.</summary>
    /// <param name="query">The query.</param>
    /// <exception cref="ArgumentException">Invalid Object in Field List</exception>
    protected internal void AddFields<TSource>(IQuery<TSource> query)
    {
        foreach (object? item in query.SelectList)
        {
            switch (item)
            {
                case string field:
                    this.QueryString.Append($"{field} ");
                    break;

                case IQuery subQuery:
                    this.QueryString.Append($"{subQuery.Build()} ");
                    break;

                default:
                    throw new ArgumentException("Invalid Field Type Specified, must be `string` or `Query`");
            }
        }

        if (query.SelectList.Count > 0)
        {
            this.QueryString.Length--;
        }
    }

    private static bool IsStringKeyValuePair(object value, out string? key, out object? val)
    {
        Type type = value.GetType();
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
        {
            Type keyType = type.GetGenericArguments()[0];
            if (keyType == typeof(string))
            {
                key = (string?)type.GetProperty("Key")!.GetValue(value);
                val = type.GetProperty("Value")!.GetValue(value);
                return true;
            }
        }

        key = null;
        val = null;
        return false;
    }

    private Dictionary<string, object?> ObjectToDictionary(object @object)
    {
        IEnumerable<PropertyInfo> properties = @object.GetType().GetProperties();

        if (this.ignoreCondition == QueryIgnoreCondition.WhenWritingNull)
        {
            properties = properties.Where(property => property.GetValue(@object) is not null);
        }
        else if (this.ignoreCondition == QueryIgnoreCondition.WhenWritingDefault)
        {
            properties = properties.Where(property => !IsDefaultValue(property, @object));
        }

        return properties
            .Select(property =>
                new KeyValuePair<string, object?>(
                    this.formatter is not null ? this.formatter.Invoke(property) : property.Name,
                    property.GetValue(@object)))
            .OrderBy(property => property.Key)
            .ToDictionary(property => property.Key, property => property.Value);
    }

    private static bool IsDefaultValue(PropertyInfo property, object @object)
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
}
