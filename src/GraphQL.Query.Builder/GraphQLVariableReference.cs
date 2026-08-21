namespace GraphQL.Query.Builder;

/// <summary>Represents a reference to a GraphQL variable ($varName) used in argument values.</summary>
public class GraphQLVariableReference
{
    /// <summary>Gets the variable name (without the $ prefix).</summary>
    public string Name { get; }

    /// <summary>Initializes a new instance of the <see cref="GraphQLVariableReference" /> class.</summary>
    /// <param name="name">The variable name (without the $ prefix).</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name.</exception>
    public GraphQLVariableReference(string name)
    {
        GraphQLNameValidator.Validate(name, nameof(name));

        this.Name = name;
    }
}
