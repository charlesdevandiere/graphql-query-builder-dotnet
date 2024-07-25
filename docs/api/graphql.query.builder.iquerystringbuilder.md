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

### **Build&lt;TSource&gt;(IQuery&lt;TSource&gt;)**

Builds the query.

```csharp
string Build<TSource>(IQuery<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IQuery&lt;TSource&gt;<br>
The query.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

---

[`< Back`](./)
