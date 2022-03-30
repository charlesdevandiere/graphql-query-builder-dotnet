using System;

namespace GraphQL.Query.Builder
{
    /// <summary>The query options class.</summary>
    public class QueryOptions
    {
        /// <summary>Gets or sets the formatter.</summary>
        public Func<string, string> Formatter { get; set; }

        /// <summary>Gets or sets the query string builder factory.</summary>
        public Func<IQueryStringBuilder> QueryStringBuilderFactory { get; set; }
    }
}
