---
layout: default
title: "GraphQLField<TSource>"
---

[`< Back`](./)

---

# GraphQLField&lt;TSource&gt;

Namespace: GraphQL.Query.Builder

The GraphQL field class.

```csharp
public class GraphQLField<TSource> : IGraphQLField`1, IGraphQLField
```

#### Type Parameters

`TSource`<br>

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLField&lt;TSource&gt;](./graphql.query.builder.graphqlfield-1)<br>
Implements IGraphQLField&lt;TSource&gt;, [IGraphQLField](./graphql.query.builder.igraphqlfield)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Fields

### **options**

The query options.

```csharp
protected QueryOptions? options;
```

## Properties

### **SelectList**

Gets the select list.

```csharp
public List<object?> SelectList { get; }
```

#### Property Value

[List&lt;Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Arguments**

Gets the arguments.

```csharp
public Dictionary<string, object?> Arguments { get; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Name**

Gets the query name.

```csharp
public string Name { get; private set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AliasName**

Gets the alias name.

```csharp
public string? AliasName { get; private set; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Directives**

Gets the directives attached to the field.

```csharp
public List<GraphQLDirective> Directives { get; }
```

#### Property Value

[List&lt;GraphQLDirective&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

## Constructors

### **GraphQLField(String)**

Initializes a new instance of the [GraphQLField&lt;TSource&gt;](./graphql.query.builder.graphqlfield-1) class.

```csharp
public GraphQLField(string name)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name.

### **GraphQLField(String, QueryOptions)**

Initializes a new instance of the [GraphQLField&lt;TSource&gt;](./graphql.query.builder.graphqlfield-1) class.

```csharp
public GraphQLField(string name, QueryOptions? options)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

`options` [QueryOptions](./graphql.query.builder.queryoptions)?<br>
The query options.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name.

## Methods

### **Alias(String)**

Sets the query alias name.

```csharp
public IGraphQLField<TSource> Alias(string alias)
```

#### Parameters

`alias` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The alias name.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The alias is not a valid GraphQL name.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;)**

Adds a field to the query.

```csharp
public IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
```

#### Type Parameters

`TProperty`<br>
The property type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>
The field selector.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddField(String)**

Adds a field to the query.

```csharp
public IGraphQLField<TSource> AddField(string field)
```

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;<br>
The field selector.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-list field to the query.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-list object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;<br>
The field selector.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(String, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(string field, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;, params GraphQLDirective[])**

Adds a field to the query with directives.

```csharp
public IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector, params GraphQLDirective[] directives)
```

#### Type Parameters

`TProperty`<br>

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddField(String, params GraphQLDirective[])**

Adds a field to the query with directives.

```csharp
public IGraphQLField<TSource> AddField(string field, params GraphQLDirective[] directives)
```

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-object field to the query with directives.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;<br>

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-list field to the query with directives.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>

#### Parameters

`selector` Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;<br>

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddField&lt;TSubSource&gt;(String, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-object field to the query with directives.

```csharp
public IGraphQLField<TSource> AddField<TSubSource>(string field, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddFragment(GraphQLFragment)**

Adds a fragment spread to the query.

```csharp
public IGraphQLField<TSource> AddFragment(GraphQLFragment fragment)
```

#### Parameters

`fragment` [GraphQLFragment](./graphql.query.builder.graphqlfragment)<br>

#### Returns

IGraphQLField&lt;TSource&gt;<br>

### **AddUnion&lt;TUnionType&gt;(String, Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
public IGraphQLField<TSource> AddUnion<TUnionType>(string typeName, Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build) where TUnionType : class, TSource
```

#### Type Parameters

`TUnionType`<br>
The union type.

#### Parameters

`typeName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The union type name.

`build` Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;<br>
The union building function.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The type name is not a valid GraphQL name.

### **AddUnion&lt;TUnionType&gt;(Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
public IGraphQLField<TSource> AddUnion<TUnionType>(Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build) where TUnionType : class, TSource
```

#### Type Parameters

`TUnionType`<br>
The union type.

#### Parameters

`build` Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;<br>
The union building function.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddArgument(String, Object)**

Adds a new argument to the query.

```csharp
public IGraphQLField<TSource> AddArgument(string key, object? value)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The argument name.

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)?<br>
The value.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddArguments(Dictionary&lt;String, Object&gt;)**

Adds arguments to the query.

```csharp
public IGraphQLField<TSource> AddArguments(Dictionary<string, object?> arguments)
```

#### Parameters

`arguments` [Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
the dictionary argument.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddArguments&lt;TArguments&gt;(TArguments)**

Adds arguments to the query.

```csharp
public IGraphQLField<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class
```

#### Type Parameters

`TArguments`<br>
The arguments object type.

#### Parameters

`arguments` TArguments<br>
The arguments object.

#### Returns

IGraphQLField&lt;TSource&gt;<br>
The query.

### **Build()**

Builds the query.

```csharp
public string Build()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a 'Name' specified in the Query

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a one or more 'Select' fields in the Query

### **BuildSelectionSet()**

Builds the field selection set, without the enclosing braces.

```csharp
public string BuildSelectionSet()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL selection set as string.

---

[`< Back`](./)
