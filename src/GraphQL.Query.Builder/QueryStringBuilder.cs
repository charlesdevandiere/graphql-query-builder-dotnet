﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Dawn;

namespace GraphQL.Query.Builder
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

                case int intValue:
                    return intValue.ToString();

                case float floatValue:
                    return floatValue.ToString(CultureInfo.CurrentCulture);

                case double doubleValue:
                    return doubleValue.ToString(CultureInfo.CurrentCulture);

                case bool booleanValue:
                    return booleanValue.ToString(CultureInfo.CurrentCulture).ToLower();

                case Enum enumValue:
                    return enumValue.ToString();

                case KeyValuePair<string, object> kvValue:
                    return $"{kvValue.Key}:{FormatQueryParam(kvValue.Value)}";

                case IList listValue:
                    StringBuilder listStr = new StringBuilder();

                    listStr.Append("[");
                    foreach (var obj in listValue)
                    {
                        listStr.Append(FormatQueryParam(obj) + ",");
                    }

                    if (listValue.Count > 0)
                    {
                        listStr.Length -= 1;
                    }

                    listStr.Append("]");

                    return listStr.ToString();

                case IDictionary dictValue:
                    StringBuilder dictStr = new StringBuilder();

                    dictStr.Append("{");
                    foreach (var dictObj in (Dictionary<string, object>)dictValue)
                    {
                        dictStr.Append(FormatQueryParam(dictObj) + ",");
                    }

                    if (dictValue.Count > 0)
                    {
                        dictStr.Length -= 1;
                    }

                    dictStr.Append("}");

                    return dictStr.ToString();

                default:
                    throw new InvalidDataException("Unsupported Query Parameter, Type Found : " + value.GetType());
            }
        }

        /// <summary>Adds query params to the query string.</summary>
        /// <param name="query">The query.</param>
        internal protected void AddParams<TSource>(IQuery<TSource> query) where TSource : class
        {
            Guard.Argument(query, nameof(query)).NotNull();

            foreach (var param in query.Arguments)
            {
                QueryString.Append($"{param.Key}:{FormatQueryParam(param.Value)},");
            }

            if (query.Arguments.Count > 0)
            {
                QueryString.Length--;
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
                        QueryString.Append($"{field} ");
                        break;

                    case IQuery subQuery:
                        QueryString.Append($"{subQuery.Build()} ");
                        break;

                    default:
                        throw new ArgumentException("Invalid Field Type Specified, must be `string` or `Query`");
                }
            }

            if (query.SelectList.Count > 0)
            {
                QueryString.Length--;
            }
        }

        /// <summary>Builds the query.</summary>
        /// <param name="query">The query.</param>
        /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
        public string Build<TSource>(IQuery<TSource> query) where TSource : class
        {
            if (!String.IsNullOrWhiteSpace(query.AliasName))
            {
                QueryString.Append($"{query.AliasName}:");
            }

            QueryString.Append(query.Name);

            if (query.Arguments.Count > 0)
            {
                QueryString.Append("(");
                AddParams(query);
                QueryString.Append(")");
            }

            if (query.SelectList.Count > 0)
            {
                QueryString.Append("{");
                AddFields(query);
                QueryString.Append("}");
            }
            else
            {
                AddFields(query);
            }

            return QueryString.ToString();
        }
    }
}
