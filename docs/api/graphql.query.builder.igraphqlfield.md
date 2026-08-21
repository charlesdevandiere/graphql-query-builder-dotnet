---
layout: default
title: IGraphQLField
---

[`< Back`](./)

---

# IGraphQLField

Namespace: GraphQL.Query.Builder

The GraphQL field interface.

```csharp
public interface IGraphQLField
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **Name**

Gets the query name.

```csharp
string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AliasName**

Gets the alias name.

```csharp
string? AliasName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Directives**

Gets the directives attached to the field.

```csharp
List<GraphQLDirective> Directives { get; }
```

#### Property Value

[List&lt;GraphQLDirective&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

## Methods

### **BuildSelectionSet()**

Builds the field selection set, without the enclosing braces.

```csharp
string BuildSelectionSet()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL selection set as string.

### **Build()**

Builds the query.

```csharp
string Build()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a 'Name' specified in the Query

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a one or more 'Select' fields in the Query

---

[`< Back`](./)
