using System.Text.RegularExpressions;

namespace GraphQL.Query.Builder;

/// <summary>Validates GraphQL names against the specification.</summary>
internal static class GraphQLNameValidator
{
    private static readonly Regex ValidNamePattern = new("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);

    /// <summary>Validates that the given name is a valid GraphQL identifier.</summary>
    /// <param name="name">The name to validate.</param>
    /// <param name="paramName">The parameter name for error messages.</param>
    /// <exception cref="ArgumentException">Thrown when the name is not a valid GraphQL identifier.</exception>
    internal static void Validate(string name, string paramName)
    {
        RequiredArgument.NotNullOrEmpty(name, paramName);

        if (!ValidNamePattern.IsMatch(name))
        {
            throw new ArgumentException($"'{name}' is not a valid GraphQL name. Names must match [a-zA-Z_][a-zA-Z0-9_]*.", paramName);
        }
    }
}
