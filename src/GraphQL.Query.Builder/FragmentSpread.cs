namespace GraphQL.Query.Builder;

/// <summary>Represents a fragment spread (...FragmentName) in a field selection.</summary>
internal class FragmentSpread
{
    /// <summary>Gets the fragment name.</summary>
    public string FragmentName { get; }

    /// <summary>Initializes a new instance of the <see cref="FragmentSpread" /> class.</summary>
    /// <param name="fragmentName">The fragment name.</param>
    public FragmentSpread(string fragmentName)
    {
        GraphQLNameValidator.Validate(fragmentName, nameof(fragmentName));

        this.FragmentName = fragmentName;
    }
}
