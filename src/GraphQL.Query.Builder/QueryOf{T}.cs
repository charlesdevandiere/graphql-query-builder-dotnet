using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GraphQL.Query.Builder.UnitTests")]
namespace GraphQL.Query.Builder;

/// <summary>The query class.</summary>
public class Query<TSource> : IQuery<TSource>
{
    private readonly QueryOptions options;

    /// <summary>Gets the select list.</summary>
    public List<object> SelectList { get; } = new List<object>();

    /// <summary>Gets the arguments.</summary>
    public Dictionary<string, object> Arguments { get; } = new Dictionary<string, object>();

    /// <summary>Gets the query name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the alias name.</summary>
    public string AliasName { get; private set; }

    /// <summary>Gets the query string builder.</summary>
    private IQueryStringBuilder QueryStringBuilder { get; set; } = new QueryStringBuilder();

    /// <summary>Initializes a new instance of the <see cref="Query{TSource}" /> class.</summary>
    public Query(string name)
    {
        RequiredArgument.NotNullOrEmpty(name, nameof(name));

        this.Name = name;
    }

    /// <summary>Initializes a new instance of the <see cref="Query{TSource}" /> class.</summary>
    public Query(string name, QueryOptions options)
    {
        RequiredArgument.NotNullOrEmpty(name, nameof(name));

        this.Name = name;
        this.options = options;
        if (options?.QueryStringBuilderFactory != null)
        {
            this.QueryStringBuilder = options.QueryStringBuilderFactory();
        }
        else if (options?.Formatter != null)
        {
            this.QueryStringBuilder = new QueryStringBuilder(options.Formatter);
        }
    }

    /// <summary>Sets the query alias name.</summary>
    /// <param name="alias">The alias name.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> Alias(string alias)
    {
        RequiredArgument.NotNullOrEmpty(alias, nameof(alias));

        this.AliasName = alias;

        return this;
    }

    /// <summary>Adds a field to the query.</summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
    {
        RequiredArgument.NotNull(selector, nameof(selector));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        this.SelectList.Add(name);

        return this;
    }

    /// <summary>Adds a field to the query.</summary>
    /// <param name="field">The field name.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddField(string field)
    {
        RequiredArgument.NotNullOrEmpty(field, nameof(field));

        this.SelectList.Add(field);

        return this;
    }

    /// <summary>Adds a sub-object field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddField<TSubSource>(
        Expression<Func<TSource, TSubSource>> selector,
        Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
        where TSubSource : class
    {
        RequiredArgument.NotNull(selector, nameof(selector));
        RequiredArgument.NotNull(build, nameof(build));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        return this.AddField(name, build);
    }

    /// <summary>Adds a sub-list field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-list object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddField<TSubSource>(
        Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
        Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
        where TSubSource : class
    {
        RequiredArgument.NotNull(selector, nameof(selector));
        RequiredArgument.NotNull(build, nameof(build));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        return this.AddField(name, build);
    }

    /// <summary>Adds a sub-object field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="field">The field name.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddField<TSubSource>(
        string field,
        Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
        where TSubSource : class
    {
        RequiredArgument.NotNullOrEmpty(field, nameof(field));
        RequiredArgument.NotNull(build, nameof(build));

        Query<TSubSource> query = new(field, this.options);
        IQuery<TSubSource> subQuery = build.Invoke(query);

        this.SelectList.Add(subQuery);

        return this;
    }

    /// <summary>Adds a new argument to the query.</summary>
    /// <param name="key">The argument name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddArgument(string key, object value)
    {
        RequiredArgument.NotNullOrEmpty(key, nameof(key));

        this.Arguments.Add(key, value);

        return this;
    }

    /// <summary>Adds arguments to the query.</summary>
    /// <param name="arguments">the dictionary argument.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddArguments(Dictionary<string, object> arguments)
    {
        RequiredArgument.NotNull(arguments, nameof(arguments));

        foreach (KeyValuePair<string, object> argument in arguments)
        {
            this.Arguments.Add(argument.Key, argument.Value);
        }

        return this;
    }

    /// <summary>Adds arguments to the query.</summary>
    /// <typeparam name="TArguments">The arguments object type.</typeparam>
    /// <param name="arguments">The arguments object.</param>
    /// <returns>The query.</returns>
    public IQuery<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class
    {
        RequiredArgument.NotNull(arguments, nameof(arguments));

        IEnumerable<PropertyInfo> properties = arguments
            .GetType()
            .GetProperties()
            .Where(property => property.GetValue(arguments) != null)
            .OrderBy(property => property.Name);
        foreach (PropertyInfo property in properties)
        {
            this.Arguments.Add(
                this.GetPropertyName(property),
                property.GetValue(arguments));
        }

        return this;
    }

    /// <summary>Builds the query.</summary>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    /// <exception cref="ArgumentException">Must have a 'Name' specified in the Query</exception>
    /// <exception cref="ArgumentException">Must have a one or more 'Select' fields in the Query</exception>
    public string Build()
    {
        this.QueryStringBuilder.Clear();

        return this.QueryStringBuilder.Build(this);
    }

    /// <summary>Gets property infos from lambda.</summary>
    /// <param name="lambda">The lambda.</param>
    /// <typeparam name="TProperty">The property.</typeparam>
    /// <returns>The property infos.</returns>
    private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TSource, TProperty>> lambda)
    {
        RequiredArgument.NotNull(lambda, nameof(lambda));

        if (lambda.Body is not MemberExpression member)
        {
            throw new ArgumentException($"Expression '{lambda}' body is not member expression.");
        }

        if (member.Member is not PropertyInfo propertyInfo)
        {
            throw new ArgumentException($"Expression '{lambda}' not refers to a property.");
        }

        if (propertyInfo.ReflectedType is null)
        {
            throw new ArgumentException($"Expression '{lambda}' not refers to a property.");
        }

        Type type = typeof(TSource);
        if (type != propertyInfo.ReflectedType && !propertyInfo.ReflectedType.IsAssignableFrom(type))
        {
            throw new ArgumentException($"Expression '{lambda}' refers to a property that is not from type {type}.");
        }

        return propertyInfo;
    }

    private string GetPropertyName(PropertyInfo property)
    {
        RequiredArgument.NotNull(property, nameof(property));

        return this.options?.Formatter is not null
            ? this.options?.Formatter.Invoke(property)
            : property.Name;
    }
}
