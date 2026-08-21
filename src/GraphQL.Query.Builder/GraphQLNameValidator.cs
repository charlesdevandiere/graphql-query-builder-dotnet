using System.Text.RegularExpressions;

namespace GraphQL.Query.Builder;

/// <summary>Validates GraphQL names against the specification.</summary>
internal static class GraphQLNameValidator
{
    /// <summary>The maximum time a name or type validation match is allowed to run.</summary>
    private static readonly TimeSpan MatchTimeout = TimeSpan.FromMilliseconds(100);

    private static readonly Regex ValidNamePattern = new("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled, MatchTimeout);

    private static readonly Regex ValidTypePattern = new(@"^\[*[a-zA-Z_][a-zA-Z0-9_]*!?(\]!?)*$", RegexOptions.Compiled, MatchTimeout);

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

    /// <summary>Validates that the given type is a valid GraphQL type reference (e.g. <c>ID!</c>, <c>[String!]!</c>).</summary>
    /// <param name="type">The type reference to validate.</param>
    /// <param name="paramName">The parameter name for error messages.</param>
    /// <exception cref="ArgumentException">Thrown when the type is not a valid GraphQL type reference.</exception>
    internal static void ValidateType(string type, string paramName)
    {
        RequiredArgument.NotNullOrEmpty(type, paramName);

        int openBrackets = 0;
        int closeBrackets = 0;
        foreach (char character in type)
        {
            if (character == '[')
            {
                openBrackets++;
            }
            else if (character == ']')
            {
                closeBrackets++;
            }
        }

        if (openBrackets != closeBrackets || !ValidTypePattern.IsMatch(type))
        {
            throw new ArgumentException($"'{type}' is not a valid GraphQL type. Types must be a name, optionally wrapped in balanced brackets and suffixed with '!'.", paramName);
        }
    }
}
