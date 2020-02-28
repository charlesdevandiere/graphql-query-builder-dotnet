# GraphQL Client Extensions

![logo](https://raw.githubusercontent.com/charlesdevandiere/graphql-client-extensions/master/logo.png)

Extensions for [GraphQL.Client](https://github.com/graphql-dotnet/graphql-client) to build graphQL queries from a C# model.

[![Build Status](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_apis/build/status/charlesdevandiere.graphql-client-extensions?branchName=master)](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_build/latest?definitionId=1&branchName=master)
![Azure DevOps coverage (branch)](https://img.shields.io/azure-devops/coverage/charlesdevandiere/charlesdevandiere/1/master)
[![Nuget](https://img.shields.io/nuget/v/GraphQL.Query.Builder.svg?color=blue&logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)
[![Downloads](https://img.shields.io/nuget/dt/GraphQL.Query.Builder.svg?logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)

## Install

Run this command with dotnet CLI:

```console
> dotnet add package GraphQL.Query.Builder
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
var query = new Query<Human>("humans") // set the name of the query
    .AddArguments(new { id = "uE78f5hq" }) // add query arguments
    .AddField(h => h.FirstName) // add firstName field
    .AddField(h => h.LastName) // add lastName field
    .AddField( // add a sub-object field
        h => h.HomePlanet, // set the name of the field
        sq => sq /// build the sub-query
            .AddField(p => p.Name)
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
You can use custom formater or JsonPropertyAttribute to change this behavior.

```csharp
class Human
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("homePlanet")]
    public Planet HomePlanet { get; set; }

    [JsonProperty("friends")]
    public IEnumerable<Human> Friends { get; set; }
}
```

```csharp
var query = new Query(options: new QueryOptions
    {
        Formater = QueryFormaters.CamelCaseFormater
    });
```

Formater's type is ```Func<string, string>```

### Run the query

You can run the query using two GraphQLCLient extension methods:

* ```Get<T>(IQuery query)```
* ```Post<T>(IQuery query)```

Example:

```csharp
using (var client = new GraphQLClient("<url>"))
{
    Human human = await client.Get<Human>(query);
}
```

## API documentation

See API documentation [here](api)
