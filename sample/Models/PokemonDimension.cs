using System.Text.Json.Serialization;

namespace Shared.Models;

public class PokemonDimension
{
    [JsonPropertyName("minimum")]
    public string Minimum { get; set; } = string.Empty;

    [JsonPropertyName("maximum")]
    public string Maximum { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{this.Minimum} - {this.Maximum}";
    }
}
