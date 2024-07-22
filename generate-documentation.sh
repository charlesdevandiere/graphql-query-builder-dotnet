#!/bin/bash

dotnet tool restore
dotnet build src/GraphQL.Query.Builder/GraphQL.Query.Builder.csproj -c Release -o out/
dotnet tool run xmldoc2md out/GraphQL.Query.Builder.dll docs/api --github-pages --back-button
