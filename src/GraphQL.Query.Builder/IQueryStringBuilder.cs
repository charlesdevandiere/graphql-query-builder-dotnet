using System;

namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Builds a GraphQL query from the Query Object. For parameters it
    /// support simple parameters, ENUMs, Lists, and Objects.
    /// For selections fields it supports sub-selects with params as above.
    ///
    /// Most all structures can be recursive, and are unwound as needed
    /// </summary>
    public interface IQueryStringBuilder
    {
        /// <summary>
        /// Clear the QueryStringBuilder and all that entails
        /// </summary>
        void Clear();

        /// <summary>
        /// Build the entire query into a string. This will take
        /// the query object and build a graphql query from it. This
        /// returns the query, but not the outer block. This is done so
        /// you can use the output to batch the queries
        /// </summary>
        /// <param name="query">The Query</param>
        /// <returns>GraphQL query string without outer block</returns>
        string Build<TSource>(IQuery<TSource> query) where TSource : class;
    }
}
