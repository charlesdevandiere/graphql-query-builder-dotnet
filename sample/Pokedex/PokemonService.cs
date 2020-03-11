using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;
using GraphQL.Query.Builder;
using Newtonsoft.Json.Linq;
using Shared.Models;

namespace Pokedex
{
    class PokemonService
    {
        private readonly string graphqlPokemonUrl;

        public PokemonService(string graphqlPokemonUrl)
        {
            this.graphqlPokemonUrl = graphqlPokemonUrl;
        }

        /// <summary>Returns a Pokemon.</summary>
        /// <param name="name">The Pokemon name.</param>
        public async Task<Pokemon> GetPokemon(string name)
        {
            var query = new Query<Pokemon>("pokemon")
                .Alias(name)
                .AddArguments(new { name })
                .AddField(p => p.Id)
                .AddField(p => p.Number)
                .AddField(p => p.Name)
                .AddField(p => p.Height, hq => hq
                    .AddField(h => h.Minimum)
                    .AddField(h => h.Maximum)
                )
                .AddField(p => p.Weight, wq => wq
                    .AddField(w => w.Minimum)
                    .AddField(w => w.Maximum)
                )
                .AddField(p => p.Types)
                .AddField(p => p.Attacks, aq => aq
                    .AddField<Attack>(a => a.Fast, fq => fq
                        .AddField(f => f.Name)
                        .AddField(f => f.Type)
                        .AddField(f => f.Damage)
                    )
                    .AddField<Attack>(a => a.Special, sq => sq
                        .AddField(f => f.Name)
                        .AddField(f => f.Type)
                        .AddField(f => f.Damage)
                    )
                );
            var request = new GraphQLRequest { Query = "{" + query.Build() + "}" };

            using var client = new GraphQLClient(this.graphqlPokemonUrl);
            GraphQLResponse response = await client.PostAsync(request);
            JToken jToken = response.Data.GetValue(name);

            return jToken.ToObject<Pokemon>();
        }

        /// <summary>Returns all Pokemons</summary>
        public async Task<IEnumerable<Pokemon>> GetAllPokemons()
        {
            var query = new Query<Pokemon>("pokemons")
                .AddArguments(new { first = 100 })
                .AddField(p => p.Id)
                .AddField(p => p.Number)
                .AddField(p => p.Name)
                .AddField(p => p.Height, hq => hq
                    .AddField(h => h.Minimum)
                    .AddField(h => h.Maximum)
                )
                .AddField(p => p.Weight, wq => wq
                    .AddField(w => w.Minimum)
                    .AddField(w => w.Maximum)
                )
                .AddField(p => p.Types);
            var request = new GraphQLRequest { Query = "{" + query.Build() + "}" };

            using var client = new GraphQLClient(this.graphqlPokemonUrl);
            GraphQLResponse response = await client.PostAsync(request);
            JToken jToken = response.Data.GetValue("pokemons");

            return jToken.ToObject<Pokemon[]>();
        }
    }
}
