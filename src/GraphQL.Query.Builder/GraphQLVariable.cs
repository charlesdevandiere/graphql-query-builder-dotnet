namespace GraphQL.Query.Builder;

/// <summary>Represents a GraphQL variable declaration.</summary>
public class GraphQLVariable
{
    /// <summary>Gets the variable name (without the $ prefix).</summary>
    public string Name { get; }

    /// <summary>Gets the GraphQL type (e.g., "ID!", "String", "Int").</summary>
    public string GraphQLType { get; }

    /// <summary>Gets the optional default value.</summary>
    public object? DefaultValue { get; }

    /// <summary>Gets whether a default value was specified.</summary>
    public bool HasDefaultValue { get; }

    /// <summary>Initializes a new instance of the <see cref="GraphQLVariable" /> class.</summary>
    /// <param name="name">The variable name (without the $ prefix).</param>
    /// <param name="graphQLType">The GraphQL type.</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name or the type is not a valid GraphQL type.</exception>
    public GraphQLVariable(string name, string graphQLType)
    {
        GraphQLNameValidator.Validate(name, nameof(name));
        GraphQLNameValidator.ValidateType(graphQLType, nameof(graphQLType));

        this.Name = name;
        this.GraphQLType = graphQLType;
    }

    /// <summary>Initializes a new instance of the <see cref="GraphQLVariable" /> class with a default value.</summary>
    /// <param name="name">The variable name (without the $ prefix).</param>
    /// <param name="graphQLType">The GraphQL type.</param>
    /// <param name="defaultValue">The default value.</param>
    public GraphQLVariable(string name, string graphQLType, object? defaultValue)
        : this(name, graphQLType)
    {
        this.DefaultValue = defaultValue;
        this.HasDefaultValue = true;
    }

    /// <summary>Creates a variable reference for use in argument values.</summary>
    /// <returns>A <see cref="GraphQLVariableReference" /> for this variable.</returns>
    public GraphQLVariableReference Reference() => new(this.Name);
}
