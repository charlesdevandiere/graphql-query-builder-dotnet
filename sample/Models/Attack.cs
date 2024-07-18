using System.Text.Json.Serialization;

namespace Shared.Models;

public class Attack
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    public override string ToString()
    {
        return $"{this.Name} (type: {this.Type}, damage: {this.Damage})";
    }
}
