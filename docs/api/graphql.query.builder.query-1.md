[`< Back`](./)

---

# Query&lt;TSource&gt;

Namespace: GraphQL.Query.Builder

The query class.

```csharp
public class Query<TSource> : IQuery`1, IQuery
```

#### Type Parameters

`TSource`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Query&lt;TSource&gt;](./graphql.query.builder.query-1)<br>
Implements IQuery&lt;TSource&gt;, [IQuery](./graphql.query.builder.iquery)

## Properties

### **SelectList**

Gets the select list.

```csharp
public List<object> SelectList { get; }
```

#### Property Value

[List&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Arguments**

Gets the arguments.

```csharp
public Dictionary<string, object> Arguments { get; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Name**

Gets the query name.

```csharp
public string Name { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AliasName**

Gets the alias name.

```csharp
public string AliasName { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **Query(String)**

Initializes a new instance of the [Query&lt;TSource&gt;](./graphql.query.builder.query-1) class.

```csharp
public Query(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Query(String, QueryOptions)**

Initializes a new instance of the [Query&lt;TSource&gt;](./graphql.query.builder.query-1) class.

```csharp
public Query(string name, QueryOptions options)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`options` [QueryOptions](./graphql.query.builder.queryoptions)<br>

## Methods

### **Alias(String)**

Sets the query alias name.

```csharp
public IQuery<TSource> Alias(string alias)
```

#### Parameters

`alias` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The alias name.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **AddField&lt;TProperty&gt;(Expression&lt;Func&lt;TSource, TProperty&gt;&gt;)**

Adds a field to the query.

```csharp
public IQuery<TSource> AddField<TProperty>(Expression<Func<TSource, TProperty>> selector)
```

#### Type Parameters

`TProperty`<br>
The property type.

#### Parameters

`selector` Expression&lt;Func&lt;TSource, TProperty&gt;&gt;<br>
The field selector.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **AddField(String)**

Adds a field to the query.

```csharp
public IQuery<TSource> AddField(string field)
```

#### Parameters

`field` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The field name.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, TSubSource&gt;&gt;, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
public IQuery<TSource> AddField<TSubSource>(Expression<Func<TSource, TSubSource>> selector, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
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

IQuery&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(Expression&lt;Func&lt;TSource, IEnumerable&lt;TSubSource&gt;&gt;&gt;, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-list field to the query.

```csharp
public IQuery<TSource> AddField<TSubSource>(Expression<Func<TSource, IEnumerable<TSubSource>>> selector, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
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

IQuery&lt;TSource&gt;<br>
The query.

### **AddField&lt;TSubSource&gt;(String, Func&lt;IQuery&lt;TSubSource&gt;, IQuery&lt;TSubSource&gt;&gt;)**

Adds a sub-object field to the query.

```csharp
public IQuery<TSource> AddField<TSubSource>(string field, Func<IQuery<TSubSource>, IQuery<TSubSource>> build)
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

IQuery&lt;TSource&gt;<br>
The query.

### **AddArgument(String, Object)**

Adds a new argument to the query.

```csharp
public IQuery<TSource> AddArgument(string key, object value)
```

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The argument name.

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **AddArguments(Dictionary&lt;String, Object&gt;)**

Adds arguments to the query.

```csharp
public IQuery<TSource> AddArguments(Dictionary<string, object> arguments)
```

#### Parameters

`arguments` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
the dictionary argument.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **AddArguments&lt;TArguments&gt;(TArguments)**

Adds arguments to the query.

```csharp
public IQuery<TSource> AddArguments<TArguments>(TArguments arguments)
```

#### Type Parameters

`TArguments`<br>
The arguments object type.

#### Parameters

`arguments` TArguments<br>
The arguments object.

#### Returns

IQuery&lt;TSource&gt;<br>
The query.

### **Build()**

Builds the query.

```csharp
public string Build()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a 'Name' specified in the Query

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a one or more 'Select' fields in the Query

---

[`< Back`](./)
