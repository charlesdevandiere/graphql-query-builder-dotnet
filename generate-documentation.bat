@echo off
dotnet build .\src\GraphQL.Query.Builder\GraphQL.Query.Builder.csproj -c Release
dotnet tool run xmldoc2md .\src\GraphQL.Query.Builder\bin\Debug\netstandard2.0\GraphQL.Query.Builder.dll .\docs\api --github-pages --back-button
