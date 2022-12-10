using System;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class CamelCasePropertyNameFormatterTests
{
    [Fact]
    public void Format_ShouldTransfomeNameIntoCamelCase()
    {
        {
            string name = CamelCasePropertyNameFormatter.FormatPropertyName.Invoke(typeof(Car).GetProperty(nameof(Car.Name)));
            Assert.Equal("name", name);
        }
    }

    [Fact]
    public void Format_ShouldThrowIfPropertyIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => CamelCasePropertyNameFormatter.FormatPropertyName.Invoke(null));
    }
}
