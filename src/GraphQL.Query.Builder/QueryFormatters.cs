using System;

namespace GraphQL.Query.Builder
{
    /// <summary>The query formatter class.</summary>
    public static class QueryFormatters
    {
        /// <summary>The camel case formatter.</summary>
        public static Func<string, string> CamelCaseFormatter = str =>
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
            {
                return str;
            }

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        };
    }
}
