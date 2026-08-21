---
layout: default
title: GraphQLVariableReference
---

[`< Back`](./)

---

# GraphQLVariableReference

Namespace: GraphQL.Query.Builder

Represents a reference to a GraphQL variable ($varName) used in argument values.

```csharp
public class GraphQLVariableReference
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Properties

### **Name**

Gets the variable name (without the $ prefix).

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **GraphQLVariableReference(String)**

Initializes a new instance of the [GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference) class.

```csharp
public GraphQLVariableReference(string name)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The variable name (without the $ prefix).

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name.

---

[`< Back`](./)
