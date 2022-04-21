# GraphQL Query Builder .NET

![logo](https://raw.githubusercontent.com/charlesdevandiere/graphql-query-builder-dotnet/master/logo.png)

A tool to build GraphQL query from a C# model.

[![Build Status](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_apis/build/status/charlesdevandiere.graphql-query-builder?branchName=master)](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_build/latest?definitionId=3&branchName=master)
![Coverage](https://img.shields.io/azure-devops/coverage/charlesdevandiere/charlesdevandiere/3/master)
[![Nuget](https://img.shields.io/nuget/v/GraphQL.Query.Builder.svg?color=blue&logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)
[![Downloads](https://img.shields.io/nuget/dt/GraphQL.Query.Builder.svg?logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)

## Install

Run this command with dotnet CLI:

```console
dotnet add package GraphQL.Query.Builder
```

## Usage

### Create a query

The query building is based on the object which returns.

#### Entities definition

In a first time, you need to create POCOs.

```csharp
class Human
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Planet HomePlanet { get; set; }
    public IEnumerable<Human> Friends { get; set; }
}

class Planet
{
    public string Name { get; set; }
}
```

#### Creation of the query

After that, you can write a query like this :

```csharp
IQuery<Human> query = new Query<Human>("humans") // set the name of the query
    .AddArguments(new { id = "uE78f5hq" }) // add query arguments
    .AddField(h => h.FirstName) // add firstName field
    .AddField(h => h.LastName) // add lastName field
    .AddField( // add a sub-object field
        h => h.HomePlanet, // set the name of the field
        sq => sq /// build the sub-query
            .AddField(p => p.Name)
    )
    .AddField<human>( // add a sub-list field
        h => h.Friends,
        sq => sq
            .AddField(f => f.FirstName)
            .AddField(f => f.LastName)
    );
```

This corresponds to :

```GraphQL
humans (id: "uE78f5hq") {
  FirstName
  LastName
  HomePlanet {
    Name
  }
  Friends {
    FirstName
    LastName
  }
}
```

By default, the `AddField()` method use the property name as field name.
You can change this behavior by providing a custom formatter.

```csharp
QueryOptions options = new()
{
    Formater = // Your custom formatter
};

IQuery<Human> query = new Query<Human>("human", options);
```

Formater's type is ```Func<PropertyInfo, string>```

There are a few existing formatters :

- [CamelCasePropertyNameFormatter](api/graphql.query.builder.camelcasepropertynameformatter.md) : transforms property name into camel-case.
- [GraphQL.Query.Builder.Formatter.SystemTextJson](https://github.com/charlesdevandiere/graphql-query-builder-formatter-systemtextjson) : use `JsonPropertyNameAttribute` value.
- [GraphQL.Query.Builder.Formatter.NewtonsoftJson](https://github.com/charlesdevandiere/graphql-query-builder-formatter-newtonsoftjson) : use `JsonPropertyAttribute` value.

### Build the query

Build the query using `Build` method:

```csharp
string queryString = query.Build();
```

## API documentation

See API documentation [here](api)
