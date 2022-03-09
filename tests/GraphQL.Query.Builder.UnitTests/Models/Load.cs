using System.Text.Json.Serialization;

namespace GraphQL.Query.Builder.UnitTests.Models
{
    public class Load
    {
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }
}
