namespace GraphQL.Query.Builder;

/// <summary>Wraps a scalar field name with one or more directives.</summary>
/// <remarks>
/// Sub-query fields carry their directives on the field itself
/// (<see cref="IGraphQLField.Directives" />), so this wrapper only ever holds a field name.
/// </remarks>
internal class DirectiveField
{
    /// <summary>Gets the field name.</summary>
    public string Field { get; }

    /// <summary>Gets the directives attached to the field.</summary>
    public List<GraphQLDirective> Directives { get; }

    /// <summary>Initializes a new instance of the <see cref="DirectiveField" /> class.</summary>
    /// <param name="field">The field name.</param>
    /// <param name="directives">The directives.</param>
    public DirectiveField(string field, List<GraphQLDirective> directives)
    {
        RequiredArgument.NotNullOrEmpty(field, nameof(field));
        RequiredArgument.NotNull(directives, nameof(directives));

        this.Field = field;
        this.Directives = directives;
    }
}
