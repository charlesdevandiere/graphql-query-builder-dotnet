using System;

namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Query interface
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets the query name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the query alias name.
        /// </summary>
        string AliasName { get; }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <returns>The GraphQL Query String, without outer enclosing block</returns>
        /// <exception cref="ArgumentException">Must have a 'Name' specified in the Query</exception>
        /// <exception cref="ArgumentException">Must have a one or more 'Select' fields in the Query</exception>
        string Build();
    }
}
