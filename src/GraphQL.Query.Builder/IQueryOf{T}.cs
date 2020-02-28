using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GraphQL.Query.Builder
{
    /// <summary>
    /// Query of TSource interface
    /// </summary>
    public interface IQuery<TSource> : IQuery where TSource : class
    {
        /// <summary>
        /// Gets the select list.
        /// </summary>
        List<object> SelectList { get; }

        /// <summary>
        /// Gets the arguments map.
        /// </summary>
        Dictionary<string, object> ArgumentsMap { get; }

        /// <summary>
        /// Sets the query alias name.
        /// </summary>
        /// <param name="alias">The alias name</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> Alias(string alias);

        /// <summary>
        /// Adds a field to the query.
        /// </summary>
        /// <typeparam name="TProperty">Property type</typeparam>
        /// <param name="selector">Field selector</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector);

        /// <summary>
        /// Adds a field to the query.
        /// </summary>
        /// <param name="field">Field name</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddField(string field);

        /// <summary>
        /// Adds a sub-object field to the query.
        /// </summary>
        /// <typeparam name="TSubSource">Sub-object type</typeparam>
        /// <param name="selector">Field selector</param>
        /// <param name="build">Sub-object query building function</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddField<TSubSource>(
            Expression<Func<TSource, TSubSource>> selector,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>
        /// Adds a sub-list field to the query.
        /// </summary>
        /// <typeparam name="TSubSource">Sub-list object type</typeparam>
        /// <param name="selector">Field selector</param>
        /// <param name="build">Sub-object query building function</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddField<TSubSource>(
            Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>
        /// Adds a sub-object field to the query.
        /// </summary>
        /// <typeparam name="TSubSource">Sub-object type</typeparam>
        /// <param name="field">Field name</param>
        /// <param name="build">Sub-object query building function</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddField<TSubSource>(
            string field,
            Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
            where TSubSource : class;

        /// <summary>
        /// Adds a new argument to the query.
        /// </summary>
        /// <param name="key">Argument name</param>
        /// <param name="value">Value</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddArgument(string key, object value);

        /// <summary>
        /// Adds arguments to the query.
        /// </summary>
        /// <param name="arguments">Dictionary argument</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddArguments(Dictionary<string, object> arguments);

        /// <sumary>
        /// Adds arguments to the query.
        /// </sumary>
        /// <typeparam name="TArguments">Arguments object type</typeparam>
        /// <param name="arguments">Arguments object</param>
        /// <returns>IQuery{TSource}</returns>
        IQuery<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class;
    }
}
