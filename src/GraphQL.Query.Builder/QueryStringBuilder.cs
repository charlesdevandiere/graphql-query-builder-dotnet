using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Builds a GraphQL query from the Query Object. For parameters it
    /// support simple parameters, ENUMs, Lists, and Objects.
    /// For selections fields it supports sub-selects with params as above.
    ///
    /// Most all structures can be recursive, and are unwound as needed
    /// </summary>
    public class QueryStringBuilder : IQueryStringBuilder
    {
        /// <summary>
        /// The query string builder.
        /// </summary>
        public StringBuilder QueryString { get; } = new StringBuilder();

        /// <summary>
        /// Clear the QueryStringBuilder and all that entails
        /// </summary>
        public void Clear()
        {
            QueryString.Clear();
        }

        /// <summary>
        /// Recurse an object which could be a primitive or more
        /// complex structure. This will return a string of the value
        /// at the current level. Recursion terminates when at a terminal
        /// (primitive).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        /// <exception cref="InvalidDataException">Invalid Object Type in Param List</exception>
        internal protected string BuildQueryParam(object value)
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
                    StringBuilder keyValueStr = new StringBuilder();

                    keyValueStr.Append($"{kvValue.Key}:{BuildQueryParam(kvValue.Value)}");
                    return keyValueStr.ToString();

                case IList listValue:
                    StringBuilder listStr = new StringBuilder();

                    listStr.Append("[");
                    bool hasList = false;
                    foreach (var obj in listValue)
                    {
                        listStr.Append(BuildQueryParam(obj) + ", ");
                        hasList = true;
                    }

                    // strip comma-space from local list if not empty

                    if (hasList)
                    {
                        listStr.Length -= 2;
                    }

                    listStr.Append("]");

                    return listStr.ToString();

                case IDictionary dictValue:
                    StringBuilder dictStr = new StringBuilder();

                    dictStr.Append("{");
                    bool hasType = false;
                    foreach (var dictObj in (Dictionary<string, object>)dictValue)
                    {
                        dictStr.Append(BuildQueryParam(dictObj) + ", ");
                        hasType = true;
                    }

                    // strip comma-space from type if not empty.
                    // Not sure if this should generate code
                    // or Toss, depends on if in GQL `name:{}` is valid in
                    // any circumstance

                    if (hasType)
                    {
                        dictStr.Length -= 2;
                    }

                    dictStr.Append("}");

                    return dictStr.ToString();

                default:
                    throw new InvalidDataException("Unsupported Query Parameter, Type Found : " + value.GetType());
            }
        }

        /// <summary>
        /// This take all parameter data
        /// and builds the string. This will look in the query and
        /// use the WhereMap for the list of data. The data can be
        /// most any type as long as it's one that we support. Will
        /// resolve nested structures
        /// </summary>
        /// <param name="query">The Query</param>
        internal protected void AddParams<TSource>(IQuery<TSource> query) where TSource : class
        {
            // safe-tee check

            if (query == null)
            {
                return;
            }

            // Build the param list from the name value pairs.
            // All entries have a `name`:`value` looking format. The
            // BuildQueryParam's will recurse any nested data elements

            bool hasParams = false;
            foreach (var param in query.ArgumentsMap)
            {
                QueryString.Append($"{param.Key}:");
                QueryString.Append(BuildQueryParam(param.Value) + ",");
                hasParams = true;
            }

            // Remove the last comma and space that always trails if we have params!

            if (hasParams)
            {
                QueryString.Length--;
            }
        }

        /// <summary>
        /// Adds fields to the query sting. This will use the SelectList
        /// structure from the query to build the graphql select list. This
        /// will recurse as needed to pick up sub-selects that can contain
        /// parameter lists.
        /// </summary>
        /// <param name="query">The Query</param>
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

        /// <summary>
        /// Build the entire query into a string. This will take
        /// the query object and build a graphql query from it. This
        /// returns the query, but not the outer block. This is done so
        /// you can use the output to batch the queries
        /// </summary>
        /// <param name="query">The Query</param>
        /// <returns>GraphQL query string without outer block</returns>
        public string Build<TSource>(IQuery<TSource> query) where TSource : class
        {
            if (!String.IsNullOrWhiteSpace(query.AliasName))
            {
                QueryString.Append($"{query.AliasName}:");
            }

            QueryString.Append(query.Name);

            if (query.ArgumentsMap.Count > 0)
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
