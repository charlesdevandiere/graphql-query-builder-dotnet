using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace Pokedex
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new PokemonService("https://graphql-pokemon2.vercel.app/");
            // The official URL https://graphql-pokemon.now.sh is actualy down.

            IEnumerable<Pokemon> pokemons;

            if (args != null && args.Length > 0)
            {
                pokemons = await Task.WhenAll(args.Select(name => service.GetPokemon(name)));
            }
            else
            {
                pokemons = await service.GetAllPokemons();
            }

            foreach (var pokemon in pokemons)
            {
                Console.WriteLine(pokemon);
            }
        }

    }
}
