[`< Back`](./)

---

# QueryStringBuilder

Namespace: GraphQL.Query.Builder

The GraphQL query builder class.

```csharp
public class QueryStringBuilder : IQueryStringBuilder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [QueryStringBuilder](./graphql.query.builder.querystringbuilder)<br>
Implements [IQueryStringBuilder](./graphql.query.builder.iquerystringbuilder)

## Properties

### **QueryString**

The query string builder.

```csharp
public StringBuilder QueryString { get; }
```

#### Property Value

[StringBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.text.stringbuilder)<br>

## Constructors

### **QueryStringBuilder()**



```csharp
public QueryStringBuilder()
```

## Methods

### **Clear()**

Clears the string builder.

```csharp
public void Clear()
```

### **FormatQueryParam(Object)**

Formats query param.
 
 Returns:
 - String: `"value"`
 - Number: `10`
 - Boolean: `true` / `false`
 - Enum: `EnumValue`
 - Key value pair: `key:"value"` / `key:10`
 - List: `["value1","value2"]` / `[1,2]`
 - Dictionary: `{a:"value",b:10}`

```csharp
protected internal string FormatQueryParam(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The formatted query param.

#### Exceptions

[InvalidDataException](https://docs.microsoft.com/en-us/dotnet/api/system.io.invaliddataexception)<br>
Invalid Object Type in Param List

### **AddParams&lt;TSource&gt;(IQuery&lt;TSource&gt;)**

Adds query params to the query string.

```csharp
protected internal void AddParams<TSource>(IQuery<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IQuery&lt;TSource&gt;<br>
The query.

### **AddFields&lt;TSource&gt;(IQuery&lt;TSource&gt;)**

Adds fields to the query sting.

```csharp
protected internal void AddFields<TSource>(IQuery<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IQuery&lt;TSource&gt;<br>
The query.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Invalid Object in Field List

### **Build&lt;TSource&gt;(IQuery&lt;TSource&gt;)**

Builds the query.

```csharp
public string Build<TSource>(IQuery<TSource> query)
```

#### Type Parameters

`TSource`<br>

#### Parameters

`query` IQuery&lt;TSource&gt;<br>
The query.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The GraphQL query as string, without outer enclosing block.

---

[`< Back`](./)
