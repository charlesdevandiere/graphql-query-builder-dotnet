---
layout: default
title: GraphQLOperation
---

[`< Back`](./)

---

# GraphQLOperation

Namespace: GraphQL.Query.Builder

Builds a complete GraphQL operation (query, mutation, or subscription).

```csharp
public class GraphQLOperation : IGraphQLOperation
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLOperation](./graphql.query.builder.graphqloperation)<br>
Implements [IGraphQLOperation](./graphql.query.builder.igraphqloperation)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Properties

### **Type**

Gets the operation type.

```csharp
public OperationType? Type { get; }
```

#### Property Value

[OperationType?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Name**

Gets the optional operation name.

```csharp
public string? Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **GraphQLOperation()**

Initializes a new instance of the [GraphQLOperation](./graphql.query.builder.graphqloperation) class (shorthand, no type).

```csharp
public GraphQLOperation()
```

### **GraphQLOperation(OperationType)**

Initializes a new instance of the [GraphQLOperation](./graphql.query.builder.graphqloperation) class.

```csharp
public GraphQLOperation(OperationType type)
```

#### Parameters

`type` [OperationType](./graphql.query.builder.operationtype)<br>
The operation type.

### **GraphQLOperation(OperationType, String)**

Initializes a new instance of the [GraphQLOperation](./graphql.query.builder.graphqloperation) class.

```csharp
public GraphQLOperation(OperationType type, string name)
```

#### Parameters

`type` [OperationType](./graphql.query.builder.operationtype)<br>
The operation type.

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The operation name.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name.

## Methods

### **AddQuery(IGraphQLField)**

Adds a root query to the operation.

```csharp
public GraphQLOperation AddQuery(IGraphQLField query)
```

#### Parameters

`query` [IGraphQLField](./graphql.query.builder.igraphqlfield)<br>
The query.

#### Returns

[GraphQLOperation](./graphql.query.builder.graphqloperation)<br>
The operation.

### **AddVariable(GraphQLVariable)**

Adds a variable declaration to the operation.

```csharp
public GraphQLOperation AddVariable(GraphQLVariable variable)
```

#### Parameters

`variable` [GraphQLVariable](./graphql.query.builder.graphqlvariable)<br>
The variable.

#### Returns

[GraphQLOperation](./graphql.query.builder.graphqloperation)<br>
The operation.

### **AddFragment(GraphQLFragment)**

Adds a fragment definition to the operation.

```csharp
public GraphQLOperation AddFragment(GraphQLFragment fragment)
```

#### Parameters

`fragment` [GraphQLFragment](./graphql.query.builder.graphqlfragment)<br>
The fragment.

#### Returns

[GraphQLOperation](./graphql.query.builder.graphqloperation)<br>
The operation.

### **Build()**

Builds the complete GraphQL operation string.

```csharp
public string Build()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL operation as a string.

#### Exceptions

[InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
The operation must have at least one query.

[InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
A shorthand operation cannot declare variables.

[InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Two different fragments share the same name.

---

[`< Back`](./)
