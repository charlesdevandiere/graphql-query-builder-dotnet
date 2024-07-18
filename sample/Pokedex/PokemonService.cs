using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Query.Builder;
using GraphQL.Query.Builder.Formatter.SystemTextJson;
using Shared.Models;

namespace Pokedex;

class PokemonService
{
    private readonly string graphqlPokemonUrl;

    private readonly QueryOptions options = new()
    {
        Formatter = SystemTextJsonPropertyNameFormatter.Format
    };

    private readonly IGraphQLWebsocketJsonSerializer serializer = new SystemTextJsonSerializer();

    /// <summary>Initializes a new instance of the <see cref="PokemonService" /> class.</summary>
    /// <param name="apiUrl">The pokemon graphQL API URL</param>
    public PokemonService(string apiUrl)
    {
        this.graphqlPokemonUrl = apiUrl;
    }

    /// <summary>Returns a Pokemon.</summary>
    /// <param name="name">The Pokemon name.</param>
    public async Task<Pokemon?> GetPokemon(string name)
    {
        IQuery<Pokemon> query = new Query<Pokemon>("pokemon", this.options)
            .AddArguments(new { name })
            .AddField(p => p.Id)
            .AddField(p => p.Number)
            .AddField(p => p.Name)
            .AddField(p => p.Height, hq => hq
                .AddField(h => h!.Minimum)
                .AddField(h => h!.Maximum)
            )
            .AddField(p => p.Weight, wq => wq
                .AddField(w => w!.Minimum)
                .AddField(w => w!.Maximum)
            )
            .AddField(p => p.Types)
            .AddField(p => p.Attacks, aq => aq
                .AddField<Attack>(a => a!.Fast, fq => fq
                    .AddField(f => f.Name)
                    .AddField(f => f.Type)
                    .AddField(f => f.Damage)
                )
                .AddField<Attack>(a => a!.Special, sq => sq
                    .AddField(f => f.Name)
                    .AddField(f => f.Type)
                    .AddField(f => f.Damage)
                )
            );
        GraphQLRequest request = new() { Query = "{" + query.Build() + "}" };

        using GraphQLHttpClient client = new(this.graphqlPokemonUrl, this.serializer);
        GraphQLResponse<PokemonResponse> response = await client.SendQueryAsync<PokemonResponse>(request);

        return response.Data.Pokemon;
    }

    /// <summary>Returns the Pokemons.</summary>
    /// <param name="names">The Pokemons names.</param>
    public async IAsyncEnumerable<Pokemon?> GetPokemons(string[] names)
    {
        foreach (string name in names)
        {
            yield return await this.GetPokemon(name);
        }
    }

    /// <summary>Returns all Pokemons</summary>
    public async Task<IEnumerable<Pokemon?>> GetAllPokemons()
    {
        IQuery<Pokemon> query = new Query<Pokemon>("pokemons", this.options)
            .AddArguments(new { first = 100 })
            .AddField(p => p.Id)
            .AddField(p => p.Number)
            .AddField(p => p.Name)
            .AddField(p => p.Height, hq => hq
                .AddField(h => h!.Minimum)
                .AddField(h => h!.Maximum)
            )
            .AddField(p => p.Weight, wq => wq
                .AddField(w => w!.Minimum)
                .AddField(w => w!.Maximum)
            )
            .AddField(p => p.Types);
        GraphQLRequest request = new() { Query = "{" + query.Build() + "}" };

        using GraphQLHttpClient client = new(this.graphqlPokemonUrl, this.serializer);
        GraphQLResponse<PokemonsResponse> response = await client.SendQueryAsync<PokemonsResponse>(request);

        return response.Data.Pokemons ?? [];
    }
}
