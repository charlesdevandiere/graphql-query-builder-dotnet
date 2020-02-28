# Query&lt;TSource&gt;

`Namespace: GraphQL.Query.Builder`

The Query Class is a simple class to build out graphQL  style queries. It will build the parameters and field lists  similar in a way you would use a SQL query builder to assemble  a query. This will maintain the response for the query

```csharp
public class Query<TSource>
    : IQuery<TSource>, IQuery
```

## Properties

| Type | Name | Summary |
| --- | --- | --- |
| `String` | AliasName | Gets the alias name. |
| `Dictionary<String, Object>` | ArgumentsMap | Gets the arguments map. |
| `String` | Name | Gets the query name. |
| `List<Object>` | SelectList | Gets the select list. |

## Methods

| Type | Name | Summary |
| --- | --- | --- |
| `IQuery<TSource>` | AddArgument(`String` key, `Object` value) | Adds a new argument to the query. |
| `IQuery<TSource>` | AddArguments(`Dictionary<String, Object>` arguments) | Adds arguments to the query. |
| `IQuery<TSource>` | AddArguments(`TArguments` arguments) | Adds arguments to the query. |
| `IQuery<TSource>` | AddField(`Expression<Func<TSource, TProperty>>` selector) | Adds a field to the query. |
| `IQuery<TSource>` | AddField(`String` field) | Adds a field to the query. |
| `IQuery<TSource>` | AddField(`Expression<Func<TSource, TSubSource>>` selector, `Func<IQuery<TSubSource>, IQuery<TSubSource>>` build) | Adds a field to the query. |
| `IQuery<TSource>` | AddField(`Expression<Func<TSource, IEnumerable<TSubSource>>>` selector, `Func<IQuery<TSubSource>, IQuery<TSubSource>>` build) | Adds a field to the query. |
| `IQuery<TSource>` | AddField(`String` field, `Func<IQuery<TSubSource>, IQuery<TSubSource>>` build) | Adds a field to the query. |
| `IQuery<TSource>` | Alias(`String` alias) | Sets the query alias name. |
| `String` | Build() | Builds the query. |

---

[`< Back`](../)
