using Newtonsoft.Json;

namespace Shared.Models
{
    public class Attack
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("damage")]
        public int Damage { get; set; }

        public override string ToString()
        {
            return $"{this.Name} (type: {this.Type}, damage: {this.Damage})";
        }
    }
}
