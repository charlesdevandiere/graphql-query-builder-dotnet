---
layout: default
title: IQueryStringBuilder
---

[`< Back`](./)

---

# IQueryStringBuilder

Namespace: GraphQL.Query.Builder

The GraphQL query builder interface.

```csharp
public interface IQueryStringBuilder
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Clear()**

Clears the string builder.

```csharp
void Clear()
```

### **Build&lt;TSource&gt;(IGraphQLField&lt;TSource&gt;)**

Builds the query.

```csharp
string Build<TSource>(IGraphQLField<TSource> query)
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
string BuildSelectionSet<TSource>(IGraphQLField<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IGraphQLField&lt;TSource&gt;<br>
The query.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL selection set as string.

---

[`< Back`](./)
