namespace GraphQL.Query.Builder.UnitTests.Models;

public abstract class Vehicule
{
    public string? Name { get; set; }
    public string? Manufacturer { get; set; }
    public decimal Price { get; set; }
    public Color? Color { get; set; }
}
