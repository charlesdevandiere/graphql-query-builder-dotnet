# Migration Guide: v2.x to v3.0

This guide covers all breaking changes and new features introduced in v3.0.

## Breaking Changes

### 1. `Query<T>` renamed to `GraphQLField<T>`

The `Query<T>` class and its interfaces have been renamed to better reflect their purpose — they build field selections, not full operations.

| v2.x        | v3.0               |
|-------------|--------------------|
| `IQuery`    | `IGraphQLField`    |
| `IQuery<T>` | `IGraphQLField<T>` |
| `Query<T>`  | `GraphQLField<T>`  |

**Before:**

```csharp
IQuery<User> query = new Query<User>("user")
    .AddField(u => u.Name);
```

**After:**

```csharp
IGraphQLField<User> query = new GraphQLField<User>("user")
    .AddField(u => u.Name);
```

### 2. Null properties are now included by default

Previously, null properties were silently dropped when using `AddArguments<T>()` or when serializing objects as argument values. Now they are included as `null`.

**Before (v2.x):**

```csharp
new Query<object>("car").AddArguments(new Car { Name = "Bee", Price = 10000 });
// Color and Manufacturer (both null) were silently dropped
// Output: car(Name:"Bee",Price:10000)
```

**After (v3.0):**

```csharp
new GraphQLField<object>("car").AddArguments(new Car { Name = "Bee", Price = 10000 });
// Null properties are now included
// Output: car(Color:null,Manufacturer:null,Name:"Bee",Price:10000)
```

**To restore the old behavior**, set `DefaultIgnoreCondition`:

```csharp
var options = new QueryOptions
{
    DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingNull
};

new GraphQLField<object>("car", options).AddArguments(new Car { Name = "Bee", Price = 10000 });
// Output: car(Name:"Bee",Price:10000)
```

**Available modes:**

| Value | Behavior |
|-------|----------|
| `QueryIgnoreCondition.Never` | Include all properties (new default) |
| `QueryIgnoreCondition.WhenWritingNull` | Skip properties with `null` values (old default) |
| `QueryIgnoreCondition.WhenWritingDefault` | Skip properties with default values (`null`, `0`, `false`, etc.) |

### 3. Duplicate argument keys now overwrite instead of throwing

`AddArgument` previously threw `ArgumentException` on duplicate keys. Now it silently overwrites the value.

**Before (v2.x):**

```csharp
query.AddArgument("id", 1);
query.AddArgument("id", 2); // throws ArgumentException
```

**After (v3.0):**

```csharp
query.AddArgument("id", 1);
query.AddArgument("id", 2); // overwrites, id = 2
```

### 4. Names are now validated

Every string that ends up in the query as an identifier is validated against the GraphQL identifier
pattern `[a-zA-Z_][a-zA-Z0-9_]*`: the `GraphQLField<T>` name, `Alias`, `AddField(string)`,
`AddArgument(string, ...)`, `AddUnion(string, ...)`, the operation name, directive names and directive
argument names, fragment names and type names, and variable names. Variable types are validated as
GraphQL type references, so brackets and `!` are allowed there (`ID!`, `[String!]!`).

```csharp
query.AddField("valid_name");   // OK
query.AddField("__typename");   // OK
query.AddField("field-name");   // throws ArgumentException
query.AddField("123field");     // throws ArgumentException

new GraphQLVariable("id", "[ID!]!");   // OK
new GraphQLVariable("id", "[ID!");     // throws ArgumentException
```

This does **not** affect lambda-based `AddField(x => x.Property)` calls — C# property names are inherently valid.

### 5. String escaping is now more thorough

Strings in argument values now escape backslashes, newlines, carriage returns, and tabs in addition to double quotes.

| Character | v2.x | v3.0 |
|-----------|------|------|
| `"` | `\"` | `\"` |
| `\` | (not escaped) | `\\` |
| newline | (not escaped) | `\n` |
| carriage return | (not escaped) | `\r` |
| tab | (not escaped) | `\t` |

## New Features

### Operation Wrapper

Wrap field selections in a full GraphQL operation with type and name:

```csharp
string result = new GraphQLOperation(OperationType.Query, "GetUser")
    .AddQuery(
        new GraphQLField<User>("user")
            .AddArgument("id", 1)
            .AddField(u => u.Name))
    .Build();
// => "query GetUser{user(id:1){Name}}"
```

Supports `Query`, `Mutation`, `Subscription`, and shorthand (no type):

```csharp
// Mutation
new GraphQLOperation(OperationType.Mutation)

// Subscription
new GraphQLOperation(OperationType.Subscription)

// Shorthand — omits the type keyword
new GraphQLOperation()
```

### Multiple Root Fields

Compose multiple fields in a single operation:

```csharp
string result = new GraphQLOperation(OperationType.Query)
    .AddQuery(new GraphQLField<User>("user").AddArgument("id", 1).AddField(u => u.Name))
    .AddQuery(new GraphQLField<Post>("posts").AddField(p => p.Title))
    .Build();
// => "query{user(id:1){Name} posts{Title}}"
```

### Variables

Define typed variables at the operation level and reference them in arguments:

```csharp
var idVar = new GraphQLVariable("id", "ID!");
var limitVar = new GraphQLVariable("limit", "Int", 10); // with default value

string result = new GraphQLOperation(OperationType.Query, "GetUser")
    .AddVariable(idVar)
    .AddVariable(limitVar)
    .AddQuery(
        new GraphQLField<User>("user")
            .AddArgument("id", idVar.Reference())
            .AddArgument("limit", limitVar.Reference())
            .AddField(u => u.Name))
    .Build();
// => "query GetUser($id:ID!,$limit:Int=10){user(id:$id,limit:$limit){Name}}"
```

### Directives

Attach `@include`, `@skip`, or custom directives to fields:

```csharp
var showVar = new GraphQLVariable("show", "Boolean!");

// @include / @skip
new GraphQLField<User>("user")
    .AddField(u => u.Name, GraphQLDirective.Include(showVar.Reference()))
    .AddField(u => u.Age, GraphQLDirective.Skip(showVar.Reference()));

// Custom directive
var cached = new GraphQLDirective("cacheControl", new Dictionary<string, object?>
{
    { "maxAge", 300 }
});
new GraphQLField<User>("user")
    .AddField(u => u.Name, cached);
// => "user{Name @cacheControl(maxAge:300)}"

// Directive on a sub-query
new GraphQLField<Car>("car")
    .AddField(
        c => c.Color,
        sq => sq.AddField(color => color!.Red).AddField(color => color!.Green),
        GraphQLDirective.Include(showVar.Reference()));
// => "car{Color @include(if:$show){Red Green}}"
```

### Fragments

Define reusable field selections and spread them across queries:

```csharp
var userFields = new GraphQLFragment("UserFields", "User",
    new GraphQLField<User>("User")
        .AddField(u => u.Name)
        .AddField(u => u.Email));

string result = new GraphQLOperation(OperationType.Query)
    .AddQuery(
        new GraphQLField<User>("user")
            .AddArgument("id", 1)
            .AddFragment(userFields))
    .AddQuery(
        new GraphQLField<User>("admin")
            .AddArgument("id", 2)
            .AddFragment(userFields))
    .AddFragment(userFields)
    .Build();
// => "query{user(id:1){...UserFields} admin(id:2){...UserFields}} fragment UserFields on User{Name Email}"
```
