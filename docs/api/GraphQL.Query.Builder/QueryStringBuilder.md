# QueryStringBuilder

`Namespace: GraphQL.Query.Builder`

Builds a GraphQL query from the Query Object. For parameters it  support simple parameters, ENUMs, Lists, and Objects.  For selections fields it supports sub-selects with params as above.    Most all structures can be recursive, and are unwound as needed

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
| `void` | AddFields(`IQuery<TSource>` query) | Adds fields to the query sting. This will use the SelectList  structure from the query to build the graphql select list. This  will recurse as needed to pick up sub-selects that can contain  parameter lists. |
| `void` | AddParams(`IQuery<TSource>` query) | This take all parameter data  and builds the string. This will look in the query and  use the WhereMap for the list of data. The data can be  most any type as long as it's one that we support. Will  resolve nested structures |
| `String` | Build(`IQuery<TSource>` query) | Build the entire query into a string. This will take  the query object and build a graphql query from it. This  returns the query, but not the outer block. This is done so  you can use the output to batch the queries |
| `String` | BuildQueryParam(`Object` value) | Recurse an object which could be a primitive or more  complex structure. This will return a string of the value  at the current level. Recursion terminates when at a terminal  (primitive). |
| `void` | Clear() | Clear the QueryStringBuilder and all that entails |

---

[`< Back`](../)
