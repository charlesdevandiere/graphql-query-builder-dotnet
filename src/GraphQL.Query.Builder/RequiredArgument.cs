using System;

namespace GraphQL.Query.Builder;

internal static class RequiredArgument
{
    /// <summary>Verifies argument is not null.</summary>
    /// <param name="param">The parameter.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <typeparam name="TArgument">The parameter type.</typeparam>
    internal static void NotNull<TArgument>(TArgument param, string paramName)
    {
        if (param is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>Verifies argument is not null or empty.</summary>
    /// <param name="param">The parameter.</param>
    /// <param name="paramName">The parameter name.</param>
    internal static void NotNullOrEmpty(string param, string paramName)
    {
        RequiredArgument.NotNull(param, paramName);

        if (param.Length == 0)
        {
            throw new ArgumentException("Value cannot be empty.", paramName);
        }
    }
}
