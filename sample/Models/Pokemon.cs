using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Shared.Models;

public class Pokemon
{
    [JsonPropertyName("attacks")]
    [JsonProperty("attacks")]
    public PokemonAttack Attacks { get; set; }

    [JsonPropertyName("height")]
    [JsonProperty("height")]
    public PokemonDimension Height { get; set; }

    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonPropertyName("number")]
    [JsonProperty("number")]
    public string Number { get; set; }

    [JsonPropertyName("types")]
    [JsonProperty("types")]
    public string[] Types { get; set; }

    [JsonPropertyName("weight")]
    [JsonProperty("weight")]
    public PokemonDimension Weight { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"{this.Number} {this.Name}");
        if (this.Height != null)
        {
            sb.AppendLine($"Height: {this.Height}");
        }
        if (this.Weight != null)
        {
            sb.AppendLine($"Weight: {this.Weight}");
        }
        if (this.Types != null && this.Types.Length > 0)
        {
            sb.AppendLine($"Types: {string.Join(", ", this.Types)}");
        }
        if (this.Attacks != null)
        {
            sb.Append(this.Attacks.ToString());
        }

        return sb.ToString();
    }
}
