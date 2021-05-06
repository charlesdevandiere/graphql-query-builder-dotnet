[`< Back`](./)

---

# IQuery

Namespace: GraphQL.Query.Builder

The query interface.

```csharp
public interface IQuery
```

## Properties

### **Name**

Gets the query name.

```csharp
public abstract string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **AliasName**

Gets the alias name.

```csharp
public abstract string AliasName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **Build()**

Builds the query.

```csharp
string Build()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a 'Name' specified in the Query

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Must have a one or more 'Select' fields in the Query

---

[`< Back`](./)
