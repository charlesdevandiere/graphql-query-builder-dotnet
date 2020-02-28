using Newtonsoft.Json;

namespace GraphQL.Query.Builder.UnitTests.Models
{
    public class Load
    {
        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
