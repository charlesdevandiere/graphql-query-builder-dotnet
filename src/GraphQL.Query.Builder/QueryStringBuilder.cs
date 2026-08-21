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
    /// <remarks>
    /// <see cref="Build{TSource}" /> and <see cref="BuildSelectionSet{TSource}" /> each swap in their own
    /// buffer for the duration of the call, so a builder instance reused across — or nested within —
    /// several builds never accumulates the output of the previous one.
    /// </remarks>
    public StringBuilder QueryString { get; private set; } = new();

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
    public string Build<TSource>(IGraphQLField<TSource> query)
    {
        RequiredArgument.NotNull(query, nameof(query));

        StringBuilder enclosing = this.BeginBuild();

        try
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

            this.AppendDirectives(query.Directives);

            if (query.SelectList.Count > 0)
            {
                this.QueryString.Append("{");
                this.AddFields(query);
                this.QueryString.Append("}");
            }

            return this.QueryString.ToString();
        }
        finally
        {
            this.EndBuild(enclosing);
        }
    }

    /// <summary>Builds the query selection set, without the enclosing braces.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The GraphQL selection set as string.</returns>
    public string BuildSelectionSet<TSource>(IGraphQLField<TSource> query)
    {
        RequiredArgument.NotNull(query, nameof(query));

        StringBuilder enclosing = this.BeginBuild();

        try
        {
            if (query.SelectList.Count > 0)
            {
                this.AddFields(query);
            }

            return this.QueryString.ToString();
        }
        finally
        {
            this.EndBuild(enclosing);
        }
    }

    /// <summary>Gives the build about to run its own buffer, and returns the one it displaces.</summary>
    /// <returns>The enclosing buffer, to hand back to <see cref="EndBuild" />.</returns>
    private StringBuilder BeginBuild()
    {
        StringBuilder enclosing = this.QueryString;
        this.QueryString = new StringBuilder();

        return enclosing;
    }

    /// <summary>Restores the buffer displaced by <see cref="BeginBuild" />.</summary>
    /// <param name="enclosing">The enclosing buffer.</param>
    private void EndBuild(StringBuilder enclosing)
    {
        this.QueryString = enclosing;
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
    ///       <term>DateTimeOffset</term>
    ///       <description><c>"2024-06-15T13:45:30.0000000+02:00"</c></description>
    ///     </item>
    ///     <item>
    ///       <term>TimeSpan</term>
    ///       <description><c>"00:05:00"</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Guid</term>
    ///       <description><c>"2c1e0e0a-0000-4000-8000-000000000001"</c></description>
    ///     </item>
    ///     <item>
    ///       <term>Uri</term>
    ///       <description><c>"https://example.com/a"</c></description>
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
    ///
    /// Objects are serialized from their public, readable, non-indexed <b>instance</b> properties.
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

            case GraphQLVariableReference varRef:
                return $"${varRef.Name}";

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
            
            case RawString rawStringValue:
                return rawStringValue.Value;

            case Enum enumValue:
                return enumValue.ToString();

            case DateTime dateTimeValue:
                return this.FormatQueryParam(dateTimeValue.ToString("o"), visited);

            case DateTimeOffset dateTimeOffsetValue:
                return this.FormatQueryParam(dateTimeOffsetValue.ToString("o"), visited);

            case TimeSpan timeSpanValue:
                return this.FormatQueryParam(timeSpanValue.ToString("c"), visited);

            case Guid guidValue:
                return this.FormatQueryParam(guidValue.ToString(), visited);

            case Uri uriValue:
                return this.FormatQueryParam(uriValue.ToString(), visited);

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
    protected internal void AddParams<TSource>(IGraphQLField<TSource> query)
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
    protected internal void AddFields<TSource>(IGraphQLField<TSource> query)
    {
        foreach (object? item in query.SelectList)
        {
            switch (item)
            {
                case string field:
                    this.QueryString.Append($"{field} ");
                    break;

                case IGraphQLField subQuery:
                    this.QueryString.Append($"{subQuery.Build()} ");
                    break;

                case DirectiveField df:
                    this.AppendDirectiveField(df);
                    this.QueryString.Append(' ');
                    break;

                case FragmentSpread spread:
                    this.QueryString.Append($"...{spread.FragmentName} ");
                    break;

                default:
                    throw new ArgumentException("Invalid Field Type Specified, must be `string`, `GraphQLField`, `DirectiveField`, or `FragmentSpread`");
            }
        }

        if (query.SelectList.Count > 0)
        {
            this.QueryString.Length--;
        }
    }

    private void AppendDirectiveField(DirectiveField df)
    {
        this.QueryString.Append(df.Field);
        this.AppendDirectives(df.Directives);
    }

    private void AppendDirectives(List<GraphQLDirective> directives)
    {
        foreach (GraphQLDirective directive in directives)
        {
            this.QueryString.Append($" @{directive.Name}");

            if (directive.Arguments.Count > 0)
            {
                this.QueryString.Append('(');
                bool first = true;
                foreach (KeyValuePair<string, object?> arg in directive.Arguments)
                {
                    if (!first)
                    {
                        this.QueryString.Append(',');
                    }

                    this.QueryString.Append($"{arg.Key}:{this.FormatQueryParam(arg.Value)}");
                    first = false;
                }
                this.QueryString.Append(')');
            }
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

    private Dictionary<string, object?> ObjectToDictionary(object @object) =>
        PropertyHelper.GetPropertyValues(@object, this.ignoreCondition, this.formatter)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
}
