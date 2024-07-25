[`< Back`](./)

---

# IQuery&lt;TSource&gt;

Namespace: GraphQL.Query.Builder

Query of TSource interface.

```csharp
public interface IQuery<TSource> : IQuery
```

#### Type Parameters

`TSource`<br>

Implements [IQuery](./graphql.query.builder.iquery)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **SelectList**

Gets the select list.

```csharp
public abstract List<object> SelectList { get; }
```

#### Property Value

[List&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Arguments**

Gets the arguments.

```csharp
public abstract Dictionary<string, object> Arguments { get; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Methods

### **Alias(String)**

Sets the query alias name.

```csharp
IQuery<TSource> Alias(string alias)
```

#### Parameters

`alias` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The alias name.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;)**

Adds a field to the query.

```csharp
IQuery<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
```

#### Type Parameters

`TProperty`<br>
The property type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>
The field selector.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddField(String)**

Adds a field to the query.

```csharp
IQuery<TSource> AddField(string field)
```

#### Parameters

`field` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
IQuery<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;<br>
The field selector.

`build` Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-list field to the query.

```csharp
IQuery<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
```

#### Type Parameters

`TSubSource`<br>
The sub-list object type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;<br>
The field selector.

`build` Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddField&lt;TSubSource&gt;(String, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
IQuery<TSource> AddField<TSubSource>(string field, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
```

#### Type Parameters

`TSubSource`<br>
The sub-object type.

#### Parameters

`field` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

`build` Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;<br>
The sub-object query building function.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddUnion&lt;TUnionType&gt;(String, Func&lt;IQuery&lt;TUnionType&gt;, IQuery&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
IQuery<TSource> AddUnion<TUnionType>(string typeName, Func<IQuery<TUnionType>, IQuery<TUnionType>> build)
```

#### Type Parameters

`TUnionType`<br>
The union type.

#### Parameters

`typeName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The union type name.

`build` Func&lt;IQuery&lt;TUnionType&gt;, IQuery&lt;TUnionType&gt;&gt;<br>
The union building function.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddUnion&lt;TUnionType&gt;(Func&lt;IQuery&lt;TUnionType&gt;, IQuery&lt;TUnionType&gt;&gt;)**

Adds an union to the query.

```csharp
IQuery<TSource> AddUnion<TUnionType>(Func<IQuery<TUnionType>, IQuery<TUnionType>> build)
```

#### Type Parameters

`TUnionType`<br>
The union type.

#### Parameters

`build` Func&lt;IQuery&lt;TUnionType&gt;, IQuery&lt;TUnionType&gt;&gt;<br>
The union building function.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddArgument(String, Object)**

Adds a new argument to the query.

```csharp
IQuery<TSource> AddArgument(string key, object value)
```

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The argument name.

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddArguments(Dictionary&lt;String, Object&gt;)**

Adds arguments to the query.

```csharp
IQuery<TSource> AddArguments(Dictionary<string, object> arguments)
```

#### Parameters

`arguments` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
the dictionary argument.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

### **AddArguments&lt;TArguments&gt;(TArguments)**

Adds arguments to the query.

```csharp
IQuery<TSource> AddArguments<TArguments>(TArguments arguments)
```

#### Type Parameters

`TArguments`<br>
The arguments object type.

#### Parameters

`arguments` TArguments<br>
The arguments object.

#### Returns

[IQuery&lt;TSource&gt;](./graphql.query.builder.iquery-1)<br>
The query.

---

[`< Back`](./)
