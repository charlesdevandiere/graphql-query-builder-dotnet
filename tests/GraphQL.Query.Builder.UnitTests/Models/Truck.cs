using Newtonsoft.Json;

namespace GraphQL.Query.Builder.UnitTests.Models
{
    public class Truck
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("wheelsNumber")]
        public int WheelsNumber { get; set; }

        [JsonProperty("load")]
        public Load Load { get; set; }
    }
}
