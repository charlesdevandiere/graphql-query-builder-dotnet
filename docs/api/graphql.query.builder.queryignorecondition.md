[`< Back`](./)

---

# QueryIgnoreCondition

Namespace: GraphQL.Query.Builder

Controls when properties with null or default values are ignored during serialization.

```csharp
public enum QueryIgnoreCondition
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [QueryIgnoreCondition](./graphql.query.builder.queryignorecondition)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Never | 0 | Property is never ignored. Null and default values are always serialized. |
| WhenWritingNull | 1 | Property is ignored when its value is null. |
| WhenWritingDefault | 2 | Property is ignored when its value is the default for its type (null for reference types, 0 for numeric types, false for bool, etc.). |

---

[`< Back`](./)
