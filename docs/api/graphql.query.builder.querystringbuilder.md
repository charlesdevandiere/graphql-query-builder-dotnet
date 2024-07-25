[`< Back`](./)

---

# QueryStringBuilder

Namespace: GraphQL.Query.Builder

The GraphQL query builder class.

```csharp
public class QueryStringBuilder : IQueryStringBuilder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [QueryStringBuilder](./graphql.query.builder.querystringbuilder)<br>
Implements [IQueryStringBuilder](./graphql.query.builder.iquerystringbuilder)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](./system.runtime.compilerservices.nullableattribute)

## Fields

### **formatter**

The property name formatter.

```csharp
protected Func<PropertyInfo, string> formatter;
```

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

Initializes a new instance of the [QueryStringBuilder](./graphql.query.builder.querystringbuilder) class.

```csharp
public QueryStringBuilder()
```

### **QueryStringBuilder(Func&lt;PropertyInfo, String&gt;)**

Initializes a new instance of the [QueryStringBuilder](./graphql.query.builder.querystringbuilder) class.

```csharp
public QueryStringBuilder(Func<PropertyInfo, string> formatter)
```

#### Parameters

`formatter` [Func&lt;PropertyInfo, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
The property name formatter

## Methods

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

### **Clear()**

Clears the string builder.

```csharp
public void Clear()
```

### **FormatQueryParam(Object)**

Formats query param.
 
 Returns:

- **null** - `null`
- **String** - `"foo"`
- **Number** - `10`
- **Boolean** - `true` or `false`
- **Enum** - `EnumValue`
- **DateTime** - `"2024-06-15T13:45:30.0000000Z"`
- **Key value pair** - `foo:"bar"` or `foo:10` ...
- **List** - `["foo","bar"]` or `[1,2]` ...
- **Dictionary** - `{foo:"bar",b:10}`
- **Object** - `{foo:"bar",b:10}`

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

---

[`< Back`](./)
