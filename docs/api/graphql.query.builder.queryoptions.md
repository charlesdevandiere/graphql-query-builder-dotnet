---
layout: default
title: QueryOptions
---

[`< Back`](./)

---

# QueryOptions

Namespace: GraphQL.Query.Builder

The query options class.

```csharp
public class QueryOptions
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [QueryOptions](./graphql.query.builder.queryoptions)

## Properties

### **Formatter**

Gets or sets the property name formatter.

```csharp
public Func<PropertyInfo, string>? Formatter { get; set; }
```

#### Property Value

[Func&lt;PropertyInfo, String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **QueryStringBuilderFactory**

Gets or sets the query string builder factory.

```csharp
public Func<IQueryStringBuilder>? QueryStringBuilderFactory { get; set; }
```

#### Property Value

[Func&lt;IQueryStringBuilder&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-1)<br>

### **DefaultIgnoreCondition**

Gets or sets the condition under which properties with null or default values are ignored during serialization.
 Defaults to [QueryIgnoreCondition.Never](./graphql.query.builder.queryignorecondition#never).

```csharp
public QueryIgnoreCondition DefaultIgnoreCondition { get; set; }
```

#### Property Value

[QueryIgnoreCondition](./graphql.query.builder.queryignorecondition)<br>

## Constructors

### **QueryOptions()**

```csharp
public QueryOptions()
```

---

[`< Back`](./)
