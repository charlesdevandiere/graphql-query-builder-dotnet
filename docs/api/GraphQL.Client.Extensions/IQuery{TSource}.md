# IQuery&lt;TSource&gt;

`Namespace: GraphQL.Query.Builder`

Query of TSource interface

```csharp
public interface IQuery<TSource>
    : IQuery
```

## Properties

| Type | Name | Summary |
| --- | --- | --- |
| `Dictionary<String, Object>` | ArgumentsMap | Gets the arguments map. |
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

---

[`< Back`](../)
