using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Shared.Models;

public class Attack
{
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonPropertyName("damage")]
    [JsonProperty("damage")]
    public int Damage { get; set; }

    public override string ToString()
    {
        return $"{this.Name} (type: {this.Type}, damage: {this.Damage})";
    }
}
