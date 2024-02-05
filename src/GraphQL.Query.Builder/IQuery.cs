using System;

namespace GraphQL.Query.Builder;

/// <summary>The query interface.</summary>
public interface IQuery
{
    /// <summary>Gets the query name.</summary>
    string Name { get; }

    /// <summary>Gets the alias name.</summary>
    string AliasName { get; }

    /// <summary>Gets or sets the query type</summary>
    QueryType Type { get; set; }

    /// <summary>Builds the query.</summary>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    /// <exception cref="ArgumentException">Must have a 'Name' specified in the Query</exception>
    /// <exception cref="ArgumentException">Must have a one or more 'Select' fields in the Query</exception>
    string Build();

    string Compile();
}
