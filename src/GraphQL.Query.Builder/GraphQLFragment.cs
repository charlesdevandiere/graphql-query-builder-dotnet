namespace GraphQL.Query.Builder;

/// <summary>Represents a named GraphQL fragment.</summary>
public class GraphQLFragment
{
    /// <summary>Gets the fragment name.</summary>
    public string Name { get; }

    /// <summary>Gets the type name the fragment applies to.</summary>
    public string TypeName { get; }

    /// <summary>Gets the query containing the fragment's field selections.</summary>
    public IGraphQLField Query { get; }

    /// <summary>Initializes a new instance of the <see cref="GraphQLFragment" /> class.</summary>
    /// <param name="name">The fragment name.</param>
    /// <param name="typeName">The type name the fragment applies to.</param>
    /// <param name="query">The query containing the fragment's field selections.</param>
    /// <exception cref="ArgumentException">The name or the type name is not a valid GraphQL name.</exception>
    public GraphQLFragment(string name, string typeName, IGraphQLField query)
    {
        GraphQLNameValidator.Validate(name, nameof(name));
        GraphQLNameValidator.Validate(typeName, nameof(typeName));
        RequiredArgument.NotNull(query, nameof(query));

        this.Name = name;
        this.TypeName = typeName;
        this.Query = query;
    }
}
