using Newtonsoft.Json;

namespace Shared.Models
{
    public class PokemonDimension
    {
        [JsonProperty("minimum")]
        public string Minimum { get; set; }
        
        [JsonProperty("maximum")]
        public string Maximum { get; set; }

        public override string ToString()
        {
            return $"{this.Minimum} - {this.Maximum}";
        }
    }
}
