# IQueryStringBuilder

`Namespace: GraphQL.Query.Builder`

Builds a GraphQL query from the Query Object. For parameters it  support simple parameters, ENUMs, Lists, and Objects.  For selections fields it supports sub-selects with params as above.    Most all structures can be recursive, and are unwound as needed

```csharp
public interface IQueryStringBuilder
```

## Methods

| Type | Name | Summary |
| --- | --- | --- |
| `String` | Build(`IQuery<TSource>` query) | Build the entire query into a string. This will take  the query object and build a graphql query from it. This  returns the query, but not the outer block. This is done so  you can use the output to batch the queries |
| `void` | Clear() | Clear the QueryStringBuilder and all that entails |

---

[`< Back`](../)
