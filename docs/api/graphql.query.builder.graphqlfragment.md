---
layout: default
title: GraphQLFragment
---

[`< Back`](./)

---

# GraphQLFragment

Namespace: GraphQL.Query.Builder

Represents a named GraphQL fragment.

```csharp
public class GraphQLFragment
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [GraphQLFragment](./graphql.query.builder.graphqlfragment)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Properties

### **Name**

Gets the fragment name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TypeName**

Gets the type name the fragment applies to.

```csharp
public string TypeName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Query**

Gets the query containing the fragment's field selections.

```csharp
public IGraphQLField Query { get; }
```

#### Property Value

[IGraphQLField](./graphql.query.builder.igraphqlfield)<br>

## Constructors

### **GraphQLFragment(String, String, IGraphQLField)**

Initializes a new instance of the [GraphQLFragment](./graphql.query.builder.graphqlfragment) class.

```csharp
public GraphQLFragment(string name, string typeName, IGraphQLField query)
```

#### Parameters

`name` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The fragment name.

`typeName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The type name the fragment applies to.

`query` [IGraphQLField](./graphql.query.builder.igraphqlfield)<br>
The query containing the fragment's field selections.

#### Exceptions

[ArgumentException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
The name or the type name is not a valid GraphQL name.

---

[`< Back`](./)
