using System.Linq.Expressions;

namespace GraphQL.Query.Builder;

/// <summary>GraphQL field of TSource interface.</summary>
public interface IGraphQLField<TSource> : IGraphQLField
{
    /// <summary>Gets the select list.</summary>
    List<object?> SelectList { get; }

    /// <summary>Gets the arguments.</summary>
    Dictionary<string, object?> Arguments { get; }

    /// <summary>Sets the query alias name.</summary>
    /// <param name="alias">The alias name.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> Alias(string alias);

    /// <summary>Adds a field to the query.</summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector);

    /// <summary>Adds a field to the query.</summary>
    /// <param name="field">The field name.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField(string field);

    /// <summary>Adds a sub-object field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, TSubSource>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?;

    /// <summary>Adds a sub-list field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-list object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?;

    /// <summary>Adds a sub-object field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="field">The field name.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        string field,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?;

    /// <summary>Adds a field to the query with directives.</summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="directives">The directives to attach to the field.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector, params GraphQLDirective[] directives);

    /// <summary>Adds a field to the query with directives.</summary>
    /// <param name="field">The field name.</param>
    /// <param name="directives">The directives to attach to the field.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField(string field, params GraphQLDirective[] directives);

    /// <summary>Adds a sub-object field to the query with directives.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <param name="directives">The directives to attach to the field.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, TSubSource>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?;

    /// <summary>Adds a sub-list field to the query with directives.</summary>
    /// <typeparam name="TSubSource">The sub-list object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <param name="directives">The directives to attach to the field.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?;

    /// <summary>Adds a sub-object field to the query with directives.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="field">The field name.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <param name="directives">The directives to attach to the field.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddField<TSubSource>(
        string field,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?;

    /// <summary>Adds a fragment spread to the query.</summary>
    /// <param name="fragment">The fragment.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddFragment(GraphQLFragment fragment);

    /// <summary>Adds an union to the query.</summary>
    /// <typeparam name="TUnionType">The union type.</typeparam>
    /// <param name="typeName">The union type name.</param>
    /// <param name="build">The union building function.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddUnion<TUnionType>(
        string typeName,
        Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build)
        where TUnionType : class?, TSource;

    /// <summary>Adds an union to the query.</summary>
    /// <typeparam name="TUnionType">The union type.</typeparam>
    /// <param name="build">The union building function.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddUnion<TUnionType>(
        Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build)
        where TUnionType : class?, TSource;

    /// <summary>Adds a new argument to the query.</summary>
    /// <param name="key">The argument name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddArgument(string key, object? value);

    /// <summary>Adds arguments to the query.</summary>
    /// <param name="arguments">the dictionary argument.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddArguments(Dictionary<string, object?> arguments);

    /// <summary>Adds arguments to the query.</summary>
    /// <typeparam name="TArguments">The arguments object type.</typeparam>
    /// <param name="arguments">The arguments object.</param>
    /// <returns>The query.</returns>
    IGraphQLField<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class;
}
