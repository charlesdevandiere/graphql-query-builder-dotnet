using Newtonsoft.Json;

namespace GraphQL.Query.Builder.Formatter.NewtonsoftJson.UnitTests;

public class Car
{
    [JsonProperty("id")]
    public int Identifier { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
}
