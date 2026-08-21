using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GraphQL.Query.Builder.UnitTests")]
namespace GraphQL.Query.Builder;

/// <summary>The GraphQL field class.</summary>
public class GraphQLField<TSource> : IGraphQLField<TSource>
{
    /// <summary>The query options.</summary>
    protected readonly QueryOptions? options;

    /// <summary>Gets the select list.</summary>
    public List<object?> SelectList { get; } = [];

    /// <summary>Gets the arguments.</summary>
    public Dictionary<string, object?> Arguments { get; } = [];

    /// <summary>Gets the query name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the alias name.</summary>
    public string? AliasName { get; private set; }

    /// <summary>Gets the directives attached to the field.</summary>
    public List<GraphQLDirective> Directives { get; } = [];

    /// <summary>Initializes a new instance of the <see cref="GraphQLField{TSource}" /> class.</summary>
    /// <param name="name">The field name.</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name.</exception>
    public GraphQLField(string name) : this(name, null) { }

    /// <summary>Initializes a new instance of the <see cref="GraphQLField{TSource}" /> class.</summary>
    /// <param name="name">The field name.</param>
    /// <param name="options">The query options.</param>
    /// <exception cref="ArgumentException">The name is not a valid GraphQL name.</exception>
    public GraphQLField(string name, QueryOptions? options)
    {
        GraphQLNameValidator.Validate(name, nameof(name));

        this.Name = name;
        this.options = options;
    }

    /// <summary>Initializes a new instance whose name is not a plain GraphQL name, such as an inline fragment.</summary>
    /// <param name="options">The query options.</param>
    /// <param name="rawName">The already validated raw field name.</param>
    private GraphQLField(QueryOptions? options, string rawName)
    {
        this.Name = rawName;
        this.options = options;
    }

    /// <summary>Creates an inline fragment field (<c>... on TypeName</c>).</summary>
    /// <param name="typeName">The type name.</param>
    /// <param name="options">The query options.</param>
    /// <returns>The inline fragment field.</returns>
    private static GraphQLField<TSource> InlineFragment(string typeName, QueryOptions? options)
    {
        GraphQLNameValidator.Validate(typeName, nameof(typeName));

        return new GraphQLField<TSource>(options, $"... on {typeName}");
    }

    /// <summary>Sets the query alias name.</summary>
    /// <param name="alias">The alias name.</param>
    /// <returns>The query.</returns>
    /// <exception cref="ArgumentException">The alias is not a valid GraphQL name.</exception>
    public IGraphQLField<TSource> Alias(string alias)
    {
        GraphQLNameValidator.Validate(alias, nameof(alias));

        this.AliasName = alias;

        return this;
    }

    /// <summary>Adds a field to the query.</summary>
    /// <typeparam name="TProperty">The property type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
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
    public IGraphQLField<TSource> AddField(string field)
    {
        GraphQLNameValidator.Validate(field, nameof(field));

        this.SelectList.Add(field);

        return this;
    }

    /// <summary>Adds a sub-object field to the query.</summary>
    /// <typeparam name="TSubSource">The sub-object type.</typeparam>
    /// <param name="selector">The field selector.</param>
    /// <param name="build">The sub-object query building function.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, TSubSource>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?
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
    public IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?
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
    public IGraphQLField<TSource> AddField<TSubSource>(
        string field,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build)
        where TSubSource : class?
    {
        GraphQLNameValidator.Validate(field, nameof(field));
        RequiredArgument.NotNull(build, nameof(build));

        GraphQLField<TSubSource> query = new(field, this.options);
        IGraphQLField<TSubSource> subQuery = build.Invoke(query);

        RequiredArgument.NotNull(subQuery, nameof(build));

        this.SelectList.Add(subQuery);

        return this;
    }

    /// <summary>Adds a field to the query with directives.</summary>
    public IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector, params GraphQLDirective[] directives)
    {
        RequiredArgument.NotNull(selector, nameof(selector));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        return this.AddField(name, directives);
    }

    /// <summary>Adds a field to the query with directives.</summary>
    public IGraphQLField<TSource> AddField(string field, params GraphQLDirective[] directives)
    {
        GraphQLNameValidator.Validate(field, nameof(field));
        RequiredArgument.NotNull(directives, nameof(directives));

        if (directives.Length > 0)
        {
            this.SelectList.Add(new DirectiveField(field, [.. directives]));
        }
        else
        {
            this.SelectList.Add(field);
        }

        return this;
    }

    /// <summary>Adds a sub-object field to the query with directives.</summary>
    public IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, TSubSource>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?
    {
        RequiredArgument.NotNull(selector, nameof(selector));
        RequiredArgument.NotNull(build, nameof(build));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        return this.AddField(name, build, directives);
    }

    /// <summary>Adds a sub-list field to the query with directives.</summary>
    public IGraphQLField<TSource> AddField<TSubSource>(
        Expression<Func<TSource, IEnumerable<TSubSource>>> selector,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?
    {
        RequiredArgument.NotNull(selector, nameof(selector));
        RequiredArgument.NotNull(build, nameof(build));

        PropertyInfo property = GetPropertyInfo(selector);
        string name = this.GetPropertyName(property);

        return this.AddField(name, build, directives);
    }

    /// <summary>Adds a sub-object field to the query with directives.</summary>
    public IGraphQLField<TSource> AddField<TSubSource>(
        string field,
        Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build,
        params GraphQLDirective[] directives)
        where TSubSource : class?
    {
        GraphQLNameValidator.Validate(field, nameof(field));
        RequiredArgument.NotNull(build, nameof(build));
        RequiredArgument.NotNull(directives, nameof(directives));

        GraphQLField<TSubSource> query = new(field, this.options);
        IGraphQLField<TSubSource> subQuery = build.Invoke(query);

        RequiredArgument.NotNull(subQuery, nameof(build));

        subQuery.Directives.AddRange(directives);

        this.SelectList.Add(subQuery);

        return this;
    }

    /// <summary>Adds a fragment spread to the query.</summary>
    public IGraphQLField<TSource> AddFragment(GraphQLFragment fragment)
    {
        RequiredArgument.NotNull(fragment, nameof(fragment));

        this.SelectList.Add(new FragmentSpread(fragment.Name));

        return this;
    }

    /// <summary>Adds an union to the query.</summary>
    /// <typeparam name="TUnionType">The union type.</typeparam>
    /// <param name="typeName">The union type name.</param>
    /// <param name="build">The union building function.</param>
    /// <returns>The query.</returns>
    /// <exception cref="ArgumentException">The type name is not a valid GraphQL name.</exception>
    public IGraphQLField<TSource> AddUnion<TUnionType>(
        string typeName,
        Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build)
        where TUnionType : class?, TSource
    {
        RequiredArgument.NotNull(build, nameof(build));

        GraphQLField<TUnionType> query = GraphQLField<TUnionType>.InlineFragment(typeName, this.options);
        IGraphQLField<TUnionType> union = build.Invoke(query);

        RequiredArgument.NotNull(union, nameof(build));

        this.SelectList.Add(union);

        return this;
    }

    /// <summary>Adds an union to the query.</summary>
    /// <typeparam name="TUnionType">The union type.</typeparam>
    /// <param name="build">The union building function.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddUnion<TUnionType>(
        Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build)
        where TUnionType : class?, TSource
    {
        RequiredArgument.NotNull(build, nameof(build));

        return this.AddUnion(typeof(TUnionType).Name, build);
    }

    /// <summary>Adds a new argument to the query.</summary>
    /// <param name="key">The argument name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddArgument(string key, object? value)
    {
        GraphQLNameValidator.Validate(key, nameof(key));

        this.Arguments[key] = value;

        return this;
    }

    /// <summary>Adds arguments to the query.</summary>
    /// <param name="arguments">the dictionary argument.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddArguments(Dictionary<string, object?> arguments)
    {
        RequiredArgument.NotNull(arguments, nameof(arguments));

        foreach (KeyValuePair<string, object?> argument in arguments)
        {
            GraphQLNameValidator.Validate(argument.Key, nameof(arguments));
            this.Arguments[argument.Key] = argument.Value;
        }

        return this;
    }

    /// <summary>Adds arguments to the query.</summary>
    /// <typeparam name="TArguments">The arguments object type.</typeparam>
    /// <param name="arguments">The arguments object.</param>
    /// <returns>The query.</returns>
    public IGraphQLField<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class
    {
        RequiredArgument.NotNull(arguments, nameof(arguments));

        QueryIgnoreCondition ignoreCondition = this.options?.DefaultIgnoreCondition ?? QueryIgnoreCondition.Never;

        foreach (KeyValuePair<string, object?> kv in PropertyHelper.GetPropertyValues(arguments, ignoreCondition, this.options?.Formatter))
        {
            this.Arguments[kv.Key] = kv.Value;
        }

        return this;
    }

    /// <summary>Builds the query.</summary>
    /// <returns>The GraphQL query as string, without outer enclosing block.</returns>
    /// <exception cref="ArgumentException">Must have a 'Name' specified in the Query</exception>
    /// <exception cref="ArgumentException">Must have a one or more 'Select' fields in the Query</exception>
    public string Build()
    {
        IQueryStringBuilder builder = this.CreateQueryStringBuilder();

        return builder.Build(this);
    }

    /// <summary>Builds the field selection set, without the enclosing braces.</summary>
    /// <returns>The GraphQL selection set as string.</returns>
    public string BuildSelectionSet()
    {
        IQueryStringBuilder builder = this.CreateQueryStringBuilder();

        return builder.BuildSelectionSet(this);
    }

    /// <summary>Creates a new query string builder instance.</summary>
    private IQueryStringBuilder CreateQueryStringBuilder()
    {
        if (this.options?.QueryStringBuilderFactory is not null)
        {
            IQueryStringBuilder builder = this.options.QueryStringBuilderFactory();

            RequiredArgument.NotNull(builder, nameof(QueryOptions.QueryStringBuilderFactory));

            return builder;
        }

        QueryIgnoreCondition ignoreCondition = this.options?.DefaultIgnoreCondition ?? QueryIgnoreCondition.Never;

        return new QueryStringBuilder(this.options?.Formatter, ignoreCondition);
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
            ? this.options.Formatter.Invoke(property)
            : property.Name;
    }
}
