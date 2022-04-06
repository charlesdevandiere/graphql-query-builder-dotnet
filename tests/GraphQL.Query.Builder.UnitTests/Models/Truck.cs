using System.Text.Json.Serialization;

namespace GraphQL.Query.Builder.UnitTests.Models
{
    public class Truck
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("wheelsNumber")]
        public int WheelsNumber { get; set; }

        [JsonPropertyName("load")]
        public Load Load { get; set; }
    }
}
