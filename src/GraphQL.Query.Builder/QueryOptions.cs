using System;

namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Query options class
    /// </summary>
    public class QueryOptions
    {
        /// <summary>
        /// Gets or sets the formater
        /// </summary>
        public Func<string, string> Formater { get; set; }

        /// <summary>
        /// Gets or sets the query string builder factory
        /// </summary>
        public Func<IQueryStringBuilder> QueryStringBuilderFactory { get; set; }
    }

    /// <summary>
    /// Query formater class
    /// </summary>
    public static class QueryFormaters
    {
        /// <summary>
        /// Camel case formater
        /// </summary>
        public static Func<string, string> CamelCaseFormater = str =>
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
            {
                return str;
            }

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        };
    }
}
