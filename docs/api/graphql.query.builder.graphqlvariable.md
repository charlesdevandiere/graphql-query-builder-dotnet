---
layout: default
title: GraphQLVariable
---

[`< Back`](./)

---

# GraphQLVariable

Namespace: GraphQL.Query.Builder

Represents a GraphQL variable declaration.

```csharp
public class GraphQLVariable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLVariable](./graphql.query.builder.graphqlvariable)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Properties

### **Name**

Gets the variable name (without the $ prefix).

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GraphQLType**

Gets the GraphQL type (e.g., "ID!", "String", "Int").

```csharp
public string GraphQLType { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DefaultValue**

Gets the optional default value.

```csharp
public object? DefaultValue { get; }
```

#### Property Value

[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

### **HasDefaultValue**

Gets whether a default value was specified.

```csharp
public bool HasDefaultValue { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **GraphQLVariable(String, String)**

Initializes a new instance of the [GraphQLVariable](./graphql.query.builder.graphqlvariable) class.

```csharp
public GraphQLVariable(string name, string graphQLType)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The variable name (without the $ prefix).

`graphQLType` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL type.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name or the type is not a valid GraphQL type.

### **GraphQLVariable(String, String, Object)**

Initializes a new instance of the [GraphQLVariable](./graphql.query.builder.graphqlvariable) class with a default value.

```csharp
public GraphQLVariable(string name, string graphQLType, object? defaultValue)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The variable name (without the $ prefix).

`graphQLType` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL type.

`defaultValue` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)?<br>
The default value.

## Methods

### **Reference()**

Creates a variable reference for use in argument values.

```csharp
public GraphQLVariableReference Reference()
```

#### Returns

[GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference)<br>
A [GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference) for this variable.

---

[`< Back`](./)
