using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Dawn;

namespace GraphQLCore.Query.Builder
{
    /// <summary>The GraphQL query builder class.</summary>
    public class QueryStringBuilder : IQueryStringBuilder
    {
        /// <summary>The query string builder.</summary>
        public StringBuilder QueryString { get; } = new StringBuilder();


        /// <summary>Clears the string builder.</summary>
        public void Clear()
        {
            QueryString.Clear();
        }

        /// <summary>
        /// Formats query param.
        /// 
        /// Returns:
        /// - String: `"value"`
        /// - Number: `10`
        /// - Boolean: `true` / `false`
        /// - Enum: `EnumValue`
        /// - Key value pair: `key:"value"` / `key:10`
        /// - List: `["value1","value2"]` / `[1,2]`
        /// - Dictionary: `{a:"value",b:10}`
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The formatted query param.</returns>
        /// <exception cref="InvalidDataException">Invalid Object Type in Param List</exception>
        internal protected string FormatQueryParam(object value)
        {
            switch (value)
            {
                case string strValue:
                    return "\"" + strValue + "\"";

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
                    return floatValue.ToString(CultureInfo.CreateSpecificCulture("en-us"));

                case double doubleValue:
                    return doubleValue.ToString(CultureInfo.CreateSpecificCulture("en-us"));

                case decimal decimalValue:
                    return decimalValue.ToString(CultureInfo.CreateSpecificCulture("en-us"));

                case bool booleanValue:
                    return booleanValue ? "true" : "false";

                case Enum enumValue:
                    return enumValue.ToString();

                case KeyValuePair<string, object> kvValue:
                    return $"{kvValue.Key}:{this.FormatQueryParam(kvValue.Value)}";

                case IDictionary<string, object> dictValue:
                    return $"{{{string.Join(",", dictValue.Select(e => this.FormatQueryParam(e)))}}}";

                case IEnumerable enumerableValue:
                    var items = new List<string>();
                    foreach (object item in enumerableValue)
                    {
                        items.Add(this.FormatQueryParam(item));
                    }
                    return $"[{string.Join(",", items)}]";

                default:
                    throw new InvalidDataException("Unsupported Query Parameter, Type Found : " + value.GetType());
            }
        }

        /// <summary>Adds query params to the query string.</summary>
        /// <param name="query">The query.</param>
        internal protected void AddParams<TSource>(IQuery<TSource> query) where TSource : class
        {
            Guard.Argument(query, nameof(query)).NotNull();

            foreach (KeyValuePair<string, object> param in query.Arguments)
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
        internal protected void AddFields<TSource>(IQuery<TSource> query) where TSource : class
        {
            foreach (object item in query.SelectList)
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

        /// <summary>Builds the query.</summary>
        /// <param name="query">The query.</param>
        /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
        public string Build<TSource>(IQuery<TSource> query) where TSource : class
        {
            if (!String.IsNullOrWhiteSpace(query.AliasName))
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
            else
            {
                this.AddFields(query);
            }

            return this.QueryString.ToString();
        }
    }
}
