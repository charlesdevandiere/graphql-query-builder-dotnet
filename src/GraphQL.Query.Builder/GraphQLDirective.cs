namespace GraphQL.Query.Builder;

/// <summary>Represents a GraphQL directive (e.g., @include, @skip).</summary>
public class GraphQLDirective
{
    /// <summary>Gets the directive name (without the @ prefix).</summary>
    public string Name { get; }

    /// <summary>Gets the directive arguments.</summary>
    public Dictionary<string, object?> Arguments { get; }

    /// <summary>Initializes a new instance of the <see cref="GraphQLDirective" /> class without arguments.</summary>
    /// <param name="name">The directive name (without the @ prefix).</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name.</exception>
    public GraphQLDirective(string name)
    {
        GraphQLNameValidator.Validate(name, nameof(name));

        this.Name = name;
        this.Arguments = [];
    }

    /// <summary>Initializes a new instance of the <see cref="GraphQLDirective" /> class with arguments.</summary>
    /// <param name="name">The directive name (without the @ prefix).</param>
    /// <param name="arguments">The directive arguments.</param>
    /// <exception cref="ArgumentException">The name or one of the argument names is not a valid GraphQL name.</exception>
    public GraphQLDirective(string name, Dictionary<string, object?> arguments)
    {
        GraphQLNameValidator.Validate(name, nameof(name));
        RequiredArgument.NotNull(arguments, nameof(arguments));

        foreach (string argumentName in arguments.Keys)
        {
            GraphQLNameValidator.Validate(argumentName, nameof(arguments));
        }

        this.Name = name;
        this.Arguments = arguments;
    }

    /// <summary>Creates an @include directive.</summary>
    /// <param name="ifVar">The variable reference for the if argument.</param>
    /// <returns>A new @include directive.</returns>
    public static GraphQLDirective Include(GraphQLVariableReference ifVar) =>
        new("include", new Dictionary<string, object?> { { "if", ifVar } });

    /// <summary>Creates a @skip directive.</summary>
    /// <param name="ifVar">The variable reference for the if argument.</param>
    /// <returns>A new @skip directive.</returns>
    public static GraphQLDirective Skip(GraphQLVariableReference ifVar) =>
        new("skip", new Dictionary<string, object?> { { "if", ifVar } });
}
