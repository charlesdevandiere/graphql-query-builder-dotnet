---
layout: default
title: GraphQLDirective
---

[`< Back`](./)

---

# GraphQLDirective

Namespace: GraphQL.Query.Builder

Represents a GraphQL directive (e.g., @include, @skip).

```csharp
public class GraphQLDirective
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLDirective](./graphql.query.builder.graphqldirective)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Properties

### **Name**

Gets the directive name (without the @ prefix).

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Arguments**

Gets the directive arguments.

```csharp
public Dictionary<string, object?> Arguments { get; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Constructors

### **GraphQLDirective(String)**

Initializes a new instance of the [GraphQLDirective](./graphql.query.builder.graphqldirective) class without arguments.

```csharp
public GraphQLDirective(string name)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directive name (without the @ prefix).

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name is not a valid GraphQL name.

### **GraphQLDirective(String, Dictionary&lt;String, Object&gt;)**

Initializes a new instance of the [GraphQLDirective](./graphql.query.builder.graphqldirective) class with arguments.

```csharp
public GraphQLDirective(string name, Dictionary<string, object?> arguments)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directive name (without the @ prefix).

`arguments` [Dictionary&lt;String, Object&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The directive arguments.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name or one of the argument names is not a valid GraphQL name.

## Methods

### **Include(GraphQLVariableReference)**

Creates an @include directive.

```csharp
public static GraphQLDirective Include(GraphQLVariableReference ifVar)
```

#### Parameters

`ifVar` [GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference)<br>
The variable reference for the if argument.

#### Returns

[GraphQLDirective](./graphql.query.builder.graphqldirective)<br>
A new @include directive.

### **Skip(GraphQLVariableReference)**

Creates a @skip directive.

```csharp
public static GraphQLDirective Skip(GraphQLVariableReference ifVar)
```

#### Parameters

`ifVar` [GraphQLVariableReference](./graphql.query.builder.graphqlvariablereference)<br>
The variable reference for the if argument.

#### Returns

[GraphQLDirective](./graphql.query.builder.graphqldirective)<br>
A new @skip directive.

---

[`< Back`](./)
