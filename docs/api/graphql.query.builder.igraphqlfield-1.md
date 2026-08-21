---
layout: default
title: "IGraphQLField<TSource>"
---

[`< Back`](./)

---

# IGraphQLField&lt;TSource&gt;

Namespace: GraphQL.Query.Builder

GraphQL field of TSource interface.

```csharp
public interface IGraphQLField<TSource> : IGraphQLField
```

#### Type Parameters

`TSource`<br>

Implements [IGraphQLField](./graphql.query.builder.igraphqlfield)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **SelectList**

Gets the select list.

```csharp
List<object?> SelectList { get; }
```

#### Property Value

[List&lt;Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Arguments**

Gets the arguments.

```csharp
Dictionary<string, object?> Arguments { get; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Methods

### **Alias(String)**

Sets the query alias name.

```csharp
IGraphQLField<TSource> Alias(string alias)
```

#### Parameters

`alias` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The alias name.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;)**

Adds a field to the query.

```csharp
IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
```

#### Type Parameters

`TProperty`<br>
The property type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>
The field selector.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField(String)**

Adds a field to the query.

```csharp
IGraphQLField<TSource> AddField(string field)
```

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
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

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-list field to the query.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
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

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(String, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(string field, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build) where TSubSource : class
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

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;, params GraphQLDirective[])**

Adds a field to the query with directives.

```csharp
IGraphQLField<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector, params GraphQLDirective[] directives)
```

#### Type Parameters

`TProperty`<br>
The property type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>
The field selector.

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>
The directives to attach to the field.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField(String, params GraphQLDirective[])**

Adds a field to the query with directives.

```csharp
IGraphQLField<TSource> AddField(string field, params GraphQLDirective[] directives)
```

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>
The directives to attach to the field.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-object field to the query with directives.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;<br>
The field selector.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>
The directives to attach to the field.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-list field to the query with directives.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-list object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;<br>
The field selector.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>
The directives to attach to the field.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(String, Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;, params GraphQLDirective[])**

Adds a sub-object field to the query with directives.

```csharp
IGraphQLField<TSource> AddField<TSubSource>(string field, Func<IGraphQLField<TSubSource>, IGraphQLField<TSubSource>> build, params GraphQLDirective[] directives) where TSubSource : class
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`field` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

`build` Func&lt;IGraphQLField&lt;TSubSource&gt;, IGraphQLField&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

`params` `directives` [GraphQLDirective[]](./graphql.query.builder.graphqldirective)<br>
The directives to attach to the field.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddFragment(GraphQLFragment)**

Adds a fragment spread to the query.

```csharp
IGraphQLField<TSource> AddFragment(GraphQLFragment fragment)
```

#### Parameters

`fragment` [GraphQLFragment](./graphql.query.builder.graphqlfragment)<br>
The fragment.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddUnion&lt;TUnionType&gt;(String, Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
IGraphQLField<TSource> AddUnion<TUnionType>(string typeName, Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build) where TUnionType : class, TSource
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

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddUnion&lt;TUnionType&gt;(Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
IGraphQLField<TSource> AddUnion<TUnionType>(Func<IGraphQLField<TUnionType>, IGraphQLField<TUnionType>> build) where TUnionType : class, TSource
```

#### Type Parameters

`TUnionType`<br>
The union type.

#### Parameters

`build` Func&lt;IGraphQLField&lt;TUnionType&gt;, IGraphQLField&lt;TUnionType&gt;&gt;<br>
The union building function.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddArgument(String, Object)**

Adds a new argument to the query.

```csharp
IGraphQLField<TSource> AddArgument(string key, object? value)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The argument name.

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)?<br>
The value.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddArguments(Dictionary&lt;String, Object&gt;)**

Adds arguments to the query.

```csharp
IGraphQLField<TSource> AddArguments(Dictionary<string, object?> arguments)
```

#### Parameters

`arguments` [Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
the dictionary argument.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

### **AddArguments&lt;TArguments&gt;(TArguments)**

Adds arguments to the query.

```csharp
IGraphQLField<TSource> AddArguments<TArguments>(TArguments arguments) where TArguments : class
```

#### Type Parameters

`TArguments`<br>
The arguments object type.

#### Parameters

`arguments` TArguments<br>
The arguments object.

#### Returns

[IGraphQLField&lt;TSource&gt;](./graphql.query.builder.igraphqlfield-1)<br>
The query.

---

[`< Back`](./)
