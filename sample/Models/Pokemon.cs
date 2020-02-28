using System.Text;
using Newtonsoft.Json;

namespace Shared.Models
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weight")]
        public PokemonDimension Weight { get; set; }

        [JsonProperty("height")]
        public PokemonDimension Height { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("attacks")]
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
