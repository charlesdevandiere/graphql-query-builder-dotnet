using System;
using Pokedex;
using Shared.Models;

PokemonService service = new("https://graphql-pokemon2.vercel.app/");

// The official URL https://graphql-pokemon.now.sh is actualy down.

if (args != null && args.Length > 0)
{
    await foreach (Pokemon? pokemon in service.GetPokemons(args))
    {
        Console.WriteLine(pokemon);
    }
}
else
{
    foreach (Pokemon? pokemon in await service.GetAllPokemons())
    {
        Console.WriteLine(pokemon);
    }
}
