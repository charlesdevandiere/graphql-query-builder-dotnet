using System;
namespace GraphQL.Query.Builder.Formatters
{
    public static class ProperCaseTypeNameFormatter
    {
        /// <summary>Formats the type name in camel case.</summary>
        /// <value>The property.</value>
        public static Func<Type, string> Formatter = type =>
        {
            RequiredArgument.NotNull(type, nameof(type));
            return char.ToUpperInvariant(type.Name[0]) + type.Name.Substring(1);
            //return char.ToLowerInvariant(type.Name[0]) + type.Name.Substring(1);
        };
    }
}

