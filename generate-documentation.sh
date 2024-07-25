#!/bin/bash

rm -rf ./out/
find ./docs/api -name "*.md" -type f -delete

dotnet tool restore
dotnet build ./src/GraphQL.Query.Builder/GraphQL.Query.Builder.csproj -c Release -o out/
dotnet xmldoc2md out/GraphQL.Query.Builder.dll --output ./docs/api --github-pages --back-button
