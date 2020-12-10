# GraphQL Query Builder Sample: Pokedex

This sample is base on the [GraphQL Pokémon](https://github.com/lucasbento/graphql-pokemon) API

## Build

### Windows

```console
> dotnet build -c Release -r win-x64
```

### Ubuntu

```console
> dotnet build -c Release -r linux-x64
```

### macOS

```console
> dotnet build -c Release -r osx-x64
```

> See all runtime identifiers [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)

## Run

(Example for Windows)

```console
> .\bin\Release\net5\win-x64\Pokedex.exe pikachu
```

```console
025 Pikachu
Height: 0.35m - 0.45m
Weight: 5.25kg - 6.75kg
Types: Electric
Fast attacks:
- Quick Attack (type: Normal, damage: 10)
- Thunder Shock (type: Electric, damage: 5)
Special attacks:
- Discharge (type: Electric, damage: 35)
- Thunder (type: Electric, damage: 100)
- Thunderbolt (type: Electric, damage: 55)
```
