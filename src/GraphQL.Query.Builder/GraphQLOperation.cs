using System.Text;

namespace GraphQL.Query.Builder;

/// <summary>Builds a complete GraphQL operation (query, mutation, or subscription).</summary>
public class GraphQLOperation : IGraphQLOperation
{
    private readonly List<IGraphQLField> queries = [];
    private readonly List<GraphQLVariable> variables = [];
    private readonly List<GraphQLFragment> fragments = [];

    /// <summary>Gets the operation type.</summary>
    public OperationType? Type { get; }

    /// <summary>Gets the optional operation name.</summary>
    public string? Name { get; }

    /// <summary>Initializes a new instance of the <see cref="GraphQLOperation" /> class (shorthand, no type).</summary>
    public GraphQLOperation() { }

    /// <summary>Initializes a new instance of the <see cref="GraphQLOperation" /> class.</summary>
    /// <param name="type">The operation type.</param>
    public GraphQLOperation(OperationType type)
    {
        this.Type = type;
    }

    /// <summary>Initializes a new instance of the <see cref="GraphQLOperation" /> class.</summary>
    /// <param name="type">The operation type.</param>
    /// <param name="name">The operation name.</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name.</exception>
    public GraphQLOperation(OperationType type, string name)
    {
        GraphQLNameValidator.Validate(name, nameof(name));

        this.Type = type;
        this.Name = name;
    }

    /// <summary>Adds a root query to the operation.</summary>
    /// <param name="query">The query.</param>
    /// <returns>The operation.</returns>
    public GraphQLOperation AddQuery(IGraphQLField query)
    {
        RequiredArgument.NotNull(query, nameof(query));

        this.queries.Add(query);

        return this;
    }

    /// <summary>Adds a variable declaration to the operation.</summary>
    /// <param name="variable">The variable.</param>
    /// <returns>The operation.</returns>
    public GraphQLOperation AddVariable(GraphQLVariable variable)
    {
        RequiredArgument.NotNull(variable, nameof(variable));

        this.variables.Add(variable);

        return this;
    }

    /// <summary>Adds a fragment definition to the operation.</summary>
    /// <param name="fragment">The fragment.</param>
    /// <returns>The operation.</returns>
    public GraphQLOperation AddFragment(GraphQLFragment fragment)
    {
        RequiredArgument.NotNull(fragment, nameof(fragment));

        this.fragments.Add(fragment);

        return this;
    }

    /// <summary>Builds the complete GraphQL operation string.</summary>
    /// <returns>The GraphQL operation as a string.</returns>
    /// <exception cref="InvalidOperationException">The operation must have at least one query.</exception>
    /// <exception cref="InvalidOperationException">A shorthand operation cannot declare variables.</exception>
    /// <exception cref="InvalidOperationException">Two different fragments share the same name.</exception>
    public string Build()
    {
        if (this.queries.Count == 0)
        {
            throw new InvalidOperationException("The operation must have at least one query.");
        }

        if (this.Type is null && this.variables.Count > 0)
        {
            throw new InvalidOperationException(
                "A shorthand operation cannot declare variables. Give the operation a type (query, mutation or subscription).");
        }

        StringBuilder sb = new();

        if (this.Type is not null)
        {
            sb.Append(this.Type.Value switch
            {
                OperationType.Query => "query",
                OperationType.Mutation => "mutation",
                OperationType.Subscription => "subscription",
                _ => throw new ArgumentOutOfRangeException()
            });

            if (!string.IsNullOrEmpty(this.Name))
            {
                sb.Append($" {this.Name}");
            }
        }

        if (this.variables.Count > 0)
        {
            sb.Append('(');
            QueryStringBuilder paramFormatter = new();

            for (int i = 0; i < this.variables.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(',');
                }

                GraphQLVariable variable = this.variables[i];
                sb.Append($"${variable.Name}:{variable.GraphQLType}");

                if (variable.HasDefaultValue)
                {
                    sb.Append($"={paramFormatter.FormatQueryParam(variable.DefaultValue)}");
                }
            }

            sb.Append(')');
        }

        sb.Append('{');
        for (int i = 0; i < this.queries.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(' ');
            }

            sb.Append(this.queries[i].Build());
        }
        sb.Append('}');

        // Fragment definitions. The same fragment may be added several times — it is emitted once —
        // but two distinct fragments cannot share a name, as spreads resolve by name.
        Dictionary<string, string> emittedFragments = [];
        foreach (GraphQLFragment fragment in this.fragments)
        {
            string definition = $" fragment {fragment.Name} on {fragment.TypeName}{{{fragment.Query.BuildSelectionSet()}}}";

            if (emittedFragments.TryGetValue(fragment.Name, out string? emitted))
            {
                if (emitted != definition)
                {
                    throw new InvalidOperationException(
                        $"Two different fragments are named '{fragment.Name}'. Fragment names must be unique within an operation.");
                }

                continue;
            }

            emittedFragments.Add(fragment.Name, definition);
            sb.Append(definition);
        }

        return sb.ToString();
    }
}
