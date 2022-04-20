# GraphQL Query Builder .NET

![logo](https://raw.githubusercontent.com/charlesdevandiere/graphql-query-builder-dotnet/master/logo.png)

A tool to build GraphQL query from a C# model.

[![Build Status](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_apis/build/status/charlesdevandiere.graphql-query-builder-dotnet?branchName=master)](https://dev.azure.com/charlesdevandiere/charlesdevandiere/_build/latest?definitionId=3&branchName=master)
![Coverage](https://img.shields.io/azure-devops/coverage/charlesdevandiere/charlesdevandiere/3/master)
[![Nuget](https://img.shields.io/nuget/v/GraphQL.Query.Builder.svg?color=blue&logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)
[![Downloads](https://img.shields.io/nuget/dt/GraphQL.Query.Builder.svg?logo=nuget)](https://www.nuget.org/packages/GraphQL.Query.Builder)

See complete documentation [here](https://charlesdevandiere.github.io/graphql-query-builder-dotnet/)

See sample [here](https://github.com/charlesdevandiere/graphql-query-builder-dotnet/tree/master/sample/Pokedex)

## Install

```console
dotnet add package GraphQL.Query.Builder
```

## Usage

```csharp
// Create the query
Query<Human> query = new("humans") // set the name of the query
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
// This corresponds to:
// humans(id: "uE78f5hq") {
//   FirstName
//   LastName
//   HomePlanet {
//     Name
//   }
//   Friends {
//     FirstName
//     LastName
//   }
// }

Console.WriteLine("{" + query.Build() + "}");
// Output:
// {humans(id:"uE78f5hq"){FirstName LastName HomePlanet{Name}Friends FirstName LastName}}
```
