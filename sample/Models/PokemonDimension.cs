using System.Text.Json.Serialization;

namespace Shared.Models;

public class PokemonDimension
{
    [JsonPropertyName("minimum")]
    public string Minimum { get; set; }

    [JsonPropertyName("maximum")]
    public string Maximum { get; set; }

    public override string ToString()
    {
        return $"{this.Minimum} - {this.Maximum}";
    }
}
