---
layout: default
title: OperationType
---

[`< Back`](./)

---

# OperationType

Namespace: GraphQL.Query.Builder

The GraphQL operation type.

```csharp
public enum OperationType
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://learn.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://learn.microsoft.com/en-us/dotnet/api/system.enum) → [OperationType](./graphql.query.builder.operationtype)<br>
Implements [IComparable](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://learn.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://learn.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Query | 0 | A read-only fetch operation. |
| Mutation | 1 | A write operation followed by a fetch. |
| Subscription | 2 | A long-lived request that fetches data in response to source events. |

---

[`< Back`](./)
