# GraphQLClientExtensions

`Namespace: GraphQL.Query.Builder`

Extensions for the `GraphQL.Client.GraphQLClient` class.

```csharp
public static class GraphQLClientExtensions
```

## Static Methods

| Type | Name | Summary |
| --- | --- | --- |
| `Task<T>` | Get(this `GraphQLClient` gqlClient, `IQuery` query, `CancellationToken` cancellationToken = null) | Send a `GraphQL.Common.Request.GraphQLRequest` via GET. |
| `Task<IReadOnlyDictionary<String, JToken>>` | GetBatch(this `GraphQLClient` gqlClient, `IQuery[]` queries, `CancellationToken` cancellationToken = null) | Send a `GraphQL.Common.Request.GraphQLRequest` composed of a query batch via GET. |
| `Task<T>` | Post(this `GraphQLClient` gqlClient, `IQuery` query, `CancellationToken` cancellationToken = null) | Send a `GraphQL.Common.Request.GraphQLRequest` via POST. |
| `Task<IReadOnlyDictionary<String, JToken>>` | PostBatch(this `GraphQLClient` gqlClient, `IQuery[]` queries, `CancellationToken` cancellationToken = null) | Send a `GraphQL.Common.Request.GraphQLRequest` composed of a query batch via POST. |

---

[`< Back`](../)
