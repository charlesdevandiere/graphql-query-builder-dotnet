using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models;

public class PokemonAttack
{
    [JsonPropertyName("fast")]
    public Attack[] Fast { get; set; }

    [JsonPropertyName("special")]
    public Attack[] Special { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        if (this.Fast != null && this.Fast.Length > 0)
        {
            sb.AppendLine("Fast attacks:");
            foreach (Attack attack in this.Fast)
            {
                sb.AppendLine($"- {attack}");
            }
        }
        if (this.Special != null && this.Special.Length > 0)
        {
            sb.AppendLine("Special attacks:");
            foreach (Attack attack in this.Special)
            {
                sb.AppendLine($"- {attack}");
            }
        }

        return sb.ToString();
    }
}
