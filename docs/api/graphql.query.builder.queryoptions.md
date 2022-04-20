[`< Back`](./)

---

# QueryOptions

Namespace: GraphQL.Query.Builder

The query options class.

```csharp
public class QueryOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [QueryOptions](./graphql.query.builder.queryoptions)

## Properties

### **Formatter**

Gets or sets the property name formatter.

```csharp
public Func<PropertyInfo, string> Formatter { get; set; }
```

#### Property Value

[Func&lt;PropertyInfo, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **QueryStringBuilderFactory**

Gets or sets the query string builder factory.

```csharp
public Func<IQueryStringBuilder> QueryStringBuilderFactory { get; set; }
```

#### Property Value

[Func&lt;IQueryStringBuilder&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

## Constructors

### **QueryOptions()**



```csharp
public QueryOptions()
```

---

[`< Back`](./)
