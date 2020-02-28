dotnet publish .\src\GraphQL.Query.Builder\GraphQL.Query.Builder.csproj -c Release -o out
dotnet tool run xmldoc2md .\out\GraphQL.Query.Builder.dll .\docs\api
