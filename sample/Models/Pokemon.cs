using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weight")]
        public PokemonDimension Weight { get; set; }

        [JsonPropertyName("height")]
        public PokemonDimension Height { get; set; }

        [JsonPropertyName("types")]
        public string[] Types { get; set; }

        [JsonPropertyName("attacks")]
        public PokemonAttack Attacks { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Number} {this.Name}");
            if (this.Height != null)
            {
                sb.AppendLine($"Height: {this.Height.ToString()}");
            }
            if (this.Weight != null)
            {
                sb.AppendLine($"Weight: {this.Weight.ToString()}");
            }
            if (this.Types != null && this.Types.Length > 0)
            {
                sb.AppendLine($"Types: {string.Join(", ", this.Types)}");
            }
            if (this.Attacks != null)
            {
                sb.AppendLine(this.Attacks.ToString());
            }

            return sb.ToString();
        }
    }
}
