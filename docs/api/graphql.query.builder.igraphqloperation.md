---
layout: default
title: IGraphQLOperation
---

[`< Back`](./)

---

# IGraphQLOperation

Namespace: GraphQL.Query.Builder

The GraphQL operation interface.

```csharp
public interface IGraphQLOperation
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Type**

Gets the operation type (query, mutation, subscription), or null for shorthand.

```csharp
OperationType? Type { get; }
```

#### Property Value

[OperationType?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Name**

Gets the optional operation name.

```csharp
string? Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **Build()**

Builds the complete GraphQL operation string.

```csharp
string Build()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL operation as a string.

---

[`< Back`](./)
