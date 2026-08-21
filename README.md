# GraphQL Query Builder .NET

![logo](https://raw.githubusercontent.com/charlesdevandiere/graphql-query-builder-dotnet/master/logo.png)

A tool to build GraphQL query from a C# model.

[![Build Status](https://github.com/charlesdevandiere/graphql-query-builder-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/charlesdevandiere/graphql-query-builder-dotnet/actions/workflows/ci.yml)
[![Nuget](https://img.shields.io/nuget/v/GraphQL.Query.Builder.svg?color=blue&logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)
[![Downloads](https://img.shields.io/nuget/dt/GraphQL.Query.Builder.svg?logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)

See complete documentation [here](https://charlesdevandiere.github.io/graphql-query-builder-dotnet/)

See sample [here](https://github.com/charlesdevandiere/graphql-query-builder-dotnet/tree/master/sample/Pokedex)

Upgrading from v2.x? See the [migration guide](https://charlesdevandiere.github.io/graphql-query-builder-dotnet/MIGRATION_v2_to_v3.md).

## Install

```shell
dotnet add package GraphQL.Query.Builder
```

## Usage

### Basic Query

```csharp
IGraphQLField<Human> field = new GraphQLField<Human>("humans")
    .AddArguments(new { id = "uE78f5hq" })
    .AddField(h => h.FirstName)
    .AddField(h => h.LastName)
    .AddField(
        h => h.HomePlanet,
        sq => sq.AddField(p => p.Name)
    )
    .AddField<Human>(
        h => h.Friends,
        sq => sq
            .AddField(f => f.FirstName)
            .AddField(f => f.LastName)
    );

string query = new GraphQLOperation(OperationType.Query)
    .AddQuery(field)
    .Build();
// Output:
// query{humans(id:"uE78f5hq"){FirstName LastName HomePlanet{Name} Friends{FirstName LastName}}}
```

### Variables

```csharp
var idVar = new GraphQLVariable("id", "ID!");

string query = new GraphQLOperation(OperationType.Query, "GetHuman")
    .AddVariable(idVar)
    .AddQuery(
        new GraphQLField<Human>("human")
            .AddArgument("id", idVar.Reference())
            .AddField(h => h.FirstName)
            .AddField(h => h.LastName))
    .Build();
// Output:
// query GetHuman($id:ID!){human(id:$id){FirstName LastName}}
```

### Directives

```csharp
var showFriends = new GraphQLVariable("showFriends", "Boolean!");

new GraphQLField<Human>("human")
    .AddField(h => h.FirstName)
    .AddField<Human>(
        h => h.Friends,
        sq => sq.AddField(f => f.FirstName),
        GraphQLDirective.Include(showFriends.Reference()));
// Field output:
// human{FirstName Friends @include(if:$showFriends){FirstName}}
```

### Fragments

```csharp
var nameFields = new GraphQLFragment("NameFields", "Human",
    new GraphQLField<Human>("Human")
        .AddField(h => h.FirstName)
        .AddField(h => h.LastName));

string query = new GraphQLOperation(OperationType.Query)
    .AddQuery(
        new GraphQLField<Human>("hero").AddFragment(nameFields))
    .AddQuery(
        new GraphQLField<Human>("villain").AddFragment(nameFields))
    .AddFragment(nameFields)
    .Build();
// Output:
// query{hero{...NameFields} villain{...NameFields}} fragment NameFields on Human{FirstName LastName}
```

### Multiple Root Fields

```csharp
string query = new GraphQLOperation(OperationType.Query)
    .AddQuery(new GraphQLField<Human>("hero").AddField(h => h.FirstName))
    .AddQuery(new GraphQLField<Droid>("droid").AddArgument("id", 1).AddField(d => d.Name))
    .Build();
// Output:
// query{hero{FirstName} droid(id:1){Name}}
```

### Mutations

```csharp
string mutation = new GraphQLOperation(OperationType.Mutation, "CreateHuman")
    .AddQuery(
        new GraphQLField<Human>("createHuman")
            .AddArgument("name", "Luke")
            .AddField(h => h.Id)
            .AddField(h => h.FirstName))
    .Build();
// Output:
// mutation CreateHuman{createHuman(name:"Luke"){Id FirstName}}
```

### Options

```csharp
// CamelCase field names
var options = new QueryOptions
{
    Formatter = CamelCasePropertyNameFormatter.Format
};

new GraphQLField<Human>("human", options)
    .AddField(h => h.FirstName);
// Field output: human{firstName}

// Control null property handling
var ignoreNullOptions = new QueryOptions
{
    DefaultIgnoreCondition = QueryIgnoreCondition.WhenWritingNull
};
```
