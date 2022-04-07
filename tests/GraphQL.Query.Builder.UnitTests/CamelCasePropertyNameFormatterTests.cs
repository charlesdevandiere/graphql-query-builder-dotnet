using System;
using System.Text;
using GraphQL.Query.Builder.UnitTests.Models;
using Xunit;

namespace GraphQL.Query.Builder.UnitTests;

public class CamelCasePropertyNameFormatterTests
{
    [Fact]
    public void Format_ShouldTransfomeNameIntoCamelCase()
    {
        {
            string name = CamelCasePropertyNameFormatter.Format.Invoke(typeof(Car).GetProperty(nameof(Car.Name)));
            Assert.Equal("name", name);
        }
    }

    [Fact]
    public void Format_ShouldThrowIfPropertyIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => CamelCasePropertyNameFormatter.Format.Invoke(null));
    }
}
