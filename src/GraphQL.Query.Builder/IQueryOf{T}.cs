using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQL.Query.Builder
{
    /// <summary>Query of TSource interface.</summary>
    public interface IQuery<TSource> : IQuery
    {
        /// <summary>Gets the select list.</summary>
        List<object> SelectList { get; }

        /// <summary>Gets the arguments.</summary>
        Dictionary<string, object> Arguments { get; }

        /// <summary>Sets the query alias name.</summary>
        /// <param name="alias">The alias name.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> Alias(string alias);

        /// <summary>Adds a field to the query.</summary>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="selector">The field selector.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector);

        /// <summary>Adds a field to the query.</summary>
        /// <param name="field">The field name.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddField(string field);

        /// <summary>Adds a sub-object field to the query.</summary>
        /// <typeparam name="TSubSource">The sub-object type.</typeparam>
        /// <param name="selector">The field selector.</param>
        /// <param name="build">The sub-object query building function.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddField<TSubSource>(
            Expression<Func<TSource, TSubSource>> selector,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>Adds a sub-list field to the query.</summary>
        /// <typeparam name="TSubSource">The sub-list object type.</typeparam>
        /// <param name="selector">The field selector.</param>
        /// <param name="build">The sub-object query building function.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddField<TSubSource>(
            Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>Adds a sub-object field to the query.</summary>
        /// <typeparam name="TSubSource">The sub-object type.</typeparam>
        /// <param name="field">The field name.</param>
        /// <param name="build">The sub-object query building function.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddField<TSubSource>(
            string field,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>Adds a new argument to the query.</summary>
        /// <param name="key">The argument name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddArgument(string key, object value);

        /// <summary>Adds arguments to the query.</summary>
        /// <param name="arguments">the dictionary argument.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddArguments(Dictionary<string, object> arguments);

        /// <summary>Adds arguments to the query.</summary>
        /// <typeparam name="TArguments">The arguments object type.</typeparam>
        /// <param name="arguments">The arguments object.</param>
        /// <returns>The query.</returns>
        IQuery<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class;
    }
}
