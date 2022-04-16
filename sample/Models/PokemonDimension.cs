using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Shared.Models;

public class PokemonDimension
{
    [JsonPropertyName("minimum")]
    [JsonProperty("minimum")]
    public string Minimum { get; set; }

    [JsonPropertyName("maximum")]
    [JsonProperty("maximum")]
    public string Maximum { get; set; }

    public override string ToString()
    {
        return $"{this.Minimum} - {this.Maximum}";
    }
}
