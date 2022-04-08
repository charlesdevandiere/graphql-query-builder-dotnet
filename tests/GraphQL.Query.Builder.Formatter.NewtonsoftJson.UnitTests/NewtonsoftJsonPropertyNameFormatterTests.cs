using GraphQL.Query.Builder.Formatter.NewtonsoftJson;
using System.Reflection;
using Xunit;

namespace GraphQL.Query.Builder.Formatter.NewtonsoftJson.UnitTests;

public class NewtonsoftJsonPropertyNameFormatterTests
{
    [Fact]
    public void Format_ShouldReturnAttributValue()
    {
        PropertyInfo property = typeof(Car).GetProperty(nameof(Car.Identifier));
        string value = NewtonsoftJsonPropertyNameFormatter.Format.Invoke(property);

        Assert.Equal("id", value);
    }
}
