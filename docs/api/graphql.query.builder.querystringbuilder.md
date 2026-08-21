---
layout: default
title: QueryStringBuilder
---

[`< Back`](./)

---

# QueryStringBuilder

Namespace: GraphQL.Query.Builder

The GraphQL query builder class.

```csharp
public class QueryStringBuilder : IQueryStringBuilder
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [QueryStringBuilder](./graphql.query.builder.querystringbuilder)<br>
Implements [IQueryStringBuilder](./graphql.query.builder.iquerystringbuilder)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Fields

### **formatter**

The property name formatter.

```csharp
protected Func<PropertyInfo, string>? formatter;
```

### **ignoreCondition**

The ignore condition for null/default properties.

```csharp
protected QueryIgnoreCondition ignoreCondition;
```

## Properties

### **QueryString**

The query string builder.

```csharp
public StringBuilder QueryString { get; private set; }
```

#### Property Value

[StringBuilder](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)<br>

**Remarks:**

[QueryStringBuilder.Build&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)](./graphql.query.builder.querystringbuilder#buildtsourceigraphqlfieldtsource) and [QueryStringBuilder.BuildSelectionSet&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)](./graphql.query.builder.querystringbuilder#buildselectionsettsourceigraphqlfieldtsource) each swap in their own
 buffer for the duration of the call, so a builder instance reused across — or nested within —
 several builds never accumulates the output of the previous one.

## Constructors

### **QueryStringBuilder()**

Initializes a new instance of the [QueryStringBuilder](./graphql.query.builder.querystringbuilder) class.

```csharp
public QueryStringBuilder()
```

### **QueryStringBuilder(Func&lt;PropertyInfo, String&gt;)**

Initializes a new instance of the [QueryStringBuilder](./graphql.query.builder.querystringbuilder) class.

```csharp
public QueryStringBuilder(Func<PropertyInfo, string> formatter)
```

#### Parameters

`formatter` [Func&lt;PropertyInfo, String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<br>
The property name formatter

### **QueryStringBuilder(Func&lt;PropertyInfo, String&gt;, QueryIgnoreCondition)**

Initializes a new instance of the [QueryStringBuilder](./graphql.query.builder.querystringbuilder) class.

```csharp
public QueryStringBuilder(Func<PropertyInfo, string>? formatter, QueryIgnoreCondition ignoreCondition)
```

#### Parameters

`formatter` [Func&lt;PropertyInfo, String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)?<br>
The property name formatter

`ignoreCondition` [QueryIgnoreCondition](./graphql.query.builder.queryignorecondition)<br>
The ignore condition for null/default properties

## Methods

### **Build&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)**

Builds the query.

```csharp
public string Build<TSource>(IGraphQLField<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IGraphQLField&lt;TSource&gt;<br>
The query.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

### **BuildSelectionSet&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)**

Builds the query selection set, without the enclosing braces.

```csharp
public string BuildSelectionSet<TSource>(IGraphQLField<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IGraphQLField&lt;TSource&gt;<br>
The query.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL selection set as string.

### **Clear()**

Clears the string builder.

```csharp
public void Clear()
```

### **FormatQueryParam(Object)**

Formats query param.
 
 Returns:

- **null** - `null`
- **String** - `"foo"`
- **Number** - `10`
- **Boolean** - `true` or `false`
- **Enum** - `EnumValue`
- **DateTime** - `"2024-06-15T13:45:30.0000000Z"`
- **DateTimeOffset** - `"2024-06-15T13:45:30.0000000+02:00"`
- **TimeSpan** - `"00:05:00"`
- **Guid** - `"2c1e0e0a-0000-4000-8000-000000000001"`
- **Uri** - `"https://example.com/a"`
- **Key value pair** - `foo:"bar"` or `foo:10` ...
- **List** - `["foo","bar"]` or `[1,2]` ...
- **Dictionary** - `{foo:"bar",b:10}`
- **Object** - `{foo:"bar",b:10}`

Objects are serialized from their public, readable, non-indexed instance properties.

```csharp
protected internal virtual string FormatQueryParam(object? value)
```

#### Parameters

`value` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)?<br>

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The formatted query param.

#### Exceptions

[InvalidDataException](https://learn.microsoft.com/en-us/dotnet/api/system.io.invaliddataexception)<br>
Invalid Object Type in Param List

### **AddParams&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)**

Adds query params to the query string.

```csharp
protected internal void AddParams<TSource>(IGraphQLField<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IGraphQLField&lt;TSource&gt;<br>
The query.

### **AddFields&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)**

Adds fields to the query sting.

```csharp
protected internal void AddFields<TSource>(IGraphQLField<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IGraphQLField&lt;TSource&gt;<br>
The query.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Invalid Object in Field List

---

[`< Back`](./)
