# QueryStringBuilder

`Namespace: GraphQL.Query.Builder`

The GraphQL query builder class.

```csharp
public class QueryStringBuilder
    : IQueryStringBuilder
```

## Properties

| Type | Name | Summary |
| --- | --- | --- |
| `StringBuilder` | QueryString | The query string builder. |

## Methods

| Type | Name | Summary |
| --- | --- | --- |
| `void` | AddFields(`IQuery<TSource>` query) | Adds fields to the query sting. |
| `void` | AddParams(`IQuery<TSource>` query) | Adds query params to the query string. |
| `String` | Build(`IQuery<TSource>` query) | Builds the query. |
| `void` | Clear() | Clears the string builder. |
| `String` | FormatQueryParam(`Object` value) | Formats query param.<br>Returns:<br>- String: `"value"`<br>- Number: `10`<br>- Boolean: `true` / `false`<br>- Enum: `EnumValue`<br>- Key value pair: `key:"value"` / `key:10`<br>- List: `["value1","value2"]` / `[1,2]`<br>- Dictionary: `{a:"value",b:10}` |

---

[`< Back`](../)
